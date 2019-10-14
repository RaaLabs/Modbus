/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Logging;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.Connectors;
using System.Threading.Tasks;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a <see cref="IAmAPullConnector">pull connector</see> for Modbus
    /// </summary>
    public class Connector : IAmAPullConnector
    {
        readonly RegistersConfiguration _registers;
        readonly IMaster _master;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="registers">The <see cref="RegistersConfiguration">configured registers</see></param>
        /// <param name="master">The <see cref="IMaster"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            RegistersConfiguration registers,
            IMaster master,
            ILogger logger)
        {
            _registers = registers;
            _logger = logger;
            _master = master;
        }


        /// <inheritdoc/>
        public Source Name => "Modbus";

        /// <inheritdoc/>
        public async Task<IEnumerable<TagDataPoint>> Pull(IEnumerable<Tag> tags)
        {
            var data = new List<TagDataPoint>();
            try
            {

                foreach (var register in _registers)
                {
                    var bytes = await _master.Read(register);
                    var byteSize = GetByteSizeFrom(register.DataType);

                    for (var byteIndex = 0; byteIndex < bytes.Length; byteIndex += byteSize)
                    {
                        var tag = $"{register.Unit}:{register.StartingAddress + byteIndex / (byteSize / 2)}";
                        var byteBatch = bytes.Skip(byteIndex).Take(byteSize).ToArray();
                        var payload = ConvertBytes(register.DataType, byteBatch);
                        data.Add(new TagDataPoint(tag, (Measurement)payload));
                        _logger.Information($"Tag: {tag}, Value : {payload}");
                    }
                }
            } catch( Exception ex )
            {
                _logger.Error(ex, "Error when pulling data from slave");
            }
            return data;
        }

        ushort GetByteSizeFrom(DataType type)
        {
            switch (type)
            {
                case DataType.Int32:
                    return 4;
                case DataType.Uint32:
                    return 4;
                case DataType.Float:
                    return 4;
            }
            return 2;
        }

        double ConvertBytes(DataType type, byte[] bytes)
        {
            switch (type)
            {
                case DataType.Int32:
                    return BitConverter.ToInt32(bytes);
                case DataType.Uint32:
                    return BitConverter.ToUInt32(bytes);
                case DataType.Float:
                    return BitConverter.ToSingle(bytes);
            }
            return 0;
        }
    }
}