/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Dolittle.Logging;
using Dolittle.TimeSeries.Modules;
using NModbus;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a <see cref="IAmAPullConnector">pull connector</see> for Modbus
    /// </summary>
    public class Connector : IAmAPullConnector
    {
        readonly ConnectorConfiguration _configuration;
        TcpClient _client;
        IModbusMaster _master;
        readonly RegistersConfiguration _registers;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="configuration">The <see cref="ConnectorConfiguration">configuration</see></param>
        /// <param name="registers">The <see cref="RegistersConfiguration">configured registers</see></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            ConnectorConfiguration configuration,
            RegistersConfiguration registers,
            ILogger logger)
        {
            _configuration = configuration;
            _registers = registers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Source Name => "Modbus";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> GetAllData()
        {
            MakeSureClientIsConnected();

            var data = new List<TagWithData>();
            foreach( (Tag tag, Register register) in _registers )
            {
                var result = _master.ReadHoldingRegisters(_configuration.Unit, register.StartingAddress, GetDataSizeFrom(register.DataType));
                Array.Reverse(result);
                var bytes = new List<byte>();
                foreach (var resultShort in result)
                    bytes.AddRange(BitConverter.GetBytes(resultShort));

                var payload = ConvertBytes(register.DataType, bytes.ToArray());
                _logger.Information($"Value : {payload}");
                
                data.Add(new TagWithData(tag, payload));
            }

            return data;
        }

        /// <inheritdoc/>
        public object GetData(Tag tag)
        {
            return 0;
        }

        object ConvertBytes(DataType type, byte[] bytes)
        {
            switch( type )
            {
                case DataType.Int32: return BitConverter.ToInt32(bytes);
                case DataType.Uint32: return BitConverter.ToUInt32(bytes);
                case DataType.Float: return BitConverter.ToSingle(bytes);
            }

            return 0;
        }

        ushort GetDataSizeFrom(DataType type)
        {
            switch( type )
            {
                case DataType.Int32: return 2;
                case DataType.Uint32: return 2;
                case DataType.Float: return 2;
            }
            return 1;
        }

        void MakeSureClientIsConnected()
        {
            if( _client != null && !_client.Connected ) 
            {
                _client.Dispose();
                _client = null;
                _master.Dispose();
                _master = null;
            }

            if (_client == null)
            {
                _client = new TcpClient(_configuration.Ip, _configuration.Port);
                var factory = new ModbusFactory();
                _master = factory.CreateMaster(_client);
            }
        }
    }
}