/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Linq;
using NModbus;
using NModbus.IO;
using Serilog;
using RaaLabs.Edge.Connectors.Modbus.Model;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace RaaLabs.Edge.Connectors.Modbus.Bridge
{
    /// <summary>
    /// Represents an implementation of <see cref="IMaster"/>
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Master : IMaster
    {
        readonly ConnectorConfiguration _configuration;
        readonly ILogger _logger;

        TcpClient _client;
        TcpClientAdapter _adapter;
        IModbusMaster _master;

        /// <summary>
        /// Initializes a new instance of <see cref="Master"/>
        /// </summary>
        /// <param name="configuration"><see cref="ConnectorConfiguration">Configuration</see></param>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public Master(ConnectorConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<byte[]> Read(Register register)
        {
            MakeSureClientIsConnected();
            _logger.Information($"Getting data from slave {register.Unit} startingAdress {register.StartingAddress} size {register.Size} as DataType {Enum.GetName(typeof(DataType), register.DataType)}");


            var size = Convert.ToUInt16(register.Size * GetDataSizeFrom(register.DataType));

            try
            {
                ushort[] result;
                switch (register.FunctionCode)
                {
                    case FunctionCode.HoldingRegister:
                        result = await DoWithTimeout(Task.Run(() => _master.ReadHoldingRegistersAsync(register.Unit, register.StartingAddress, size)), _configuration.ReadTimeout);

                        break;
                    case FunctionCode.InputRegister:
                        result = await DoWithTimeout(Task.Run(() => _master.ReadInputRegistersAsync(register.Unit, register.StartingAddress, size)), _configuration.ReadTimeout);

                        break;
                    default:
                        result = Array.Empty<ushort>();
                        break;
                }
                var bytes = result.GetBytes(_configuration.Endianness, register.DataType).ToArray();
                return bytes;

            }
            catch (OperationCanceledException canceled)
            {
                _logger.Error(canceled, $"Read operation cancelled while reading register {register}");
                _client?.Close();
                _client?.Dispose();
                _client = null;

                throw new ModbusMasterException($"Read operation cancelled while reading register {register}", canceled);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Trouble reading register {register}");
                _client?.Close();
                _client?.Dispose();
                _client = null;

                throw new ModbusMasterException($"Trouble reading register {register}", ex);
            }
        }

        static async Task<T> DoWithTimeout<T>(Task<T> task, int timeout)
        {
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await task;
            }
            else
            {
                throw new OperationCanceledException();
            }
        }

        private static ushort GetDataSizeFrom(DataType type)
        {
            switch (type)
            {
                case DataType.Int32:
                    return 2;
                case DataType.Uint32:
                    return 2;
                case DataType.Float:
                    return 2;
            }
            return 1;
        }

        void MakeSureClientIsConnected()
        {
            if (_client != null && !_client.Connected)
            {
                _client.Dispose();
                _client = null;
                _adapter.Dispose();
                _adapter = null;
                _master.Dispose();
                _master = null;
            }

            if (_client == null)
            {
                _client = new TcpClient(_configuration.Ip, _configuration.Port);
                _adapter = new TcpClientAdapter(_client);
                var factory = new ModbusFactory();
                if (_configuration.UseASCII) _master = factory.CreateAsciiMaster(_adapter);
                else if (_configuration.Protocol == Protocol.Tcp) _master = factory.CreateMaster(_client);
                else _master = factory.CreateRtuMaster(_adapter);
            }
        }

        [Serializable]
        public class ModbusMasterException : Exception
        {
            public ModbusMasterException(string message) : base(message)
            {

            }

            public ModbusMasterException(string message, Exception inner) : base(message, inner)
            {

            }

            protected ModbusMasterException(SerializationInfo info, StreamingContext context) : base(info, context)
            {

            }
        }
    }
}