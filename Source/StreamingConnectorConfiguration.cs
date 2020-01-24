/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;

namespace RaaLabs.TimeSeries.Modbus
{

    /// <summary>
    /// Represents the configuration for <see cref="Connector"/>
    /// </summary>
    [Name("streamingConnectors")]
    public class StreamingConnectorConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorConfiguration"/>
        /// </summary>
        /// <param name="modbus">The configuration for the modbus connector</param>
        public StreamingConnectorConfiguration(ModbusConfiguration modbus)
        {
            Modbus = modbus;
        }

        /// <summary>
        /// Object holding modbus-specific configuration.
        /// </summary>
        public ModbusConfiguration Modbus { get; }

        /// <summary>
        /// 
        /// </summary>
        public class ModbusConfiguration
        {
            /// <summary>
            /// The polling interval for the connector.
            /// </summary>
            public int Interval { get; set; }
        }
    }
}