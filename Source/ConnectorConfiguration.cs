/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;

namespace Dolittle.TimeSeries.Modbus
{

    /// <summary>
    /// Represents the configuration for <see cref="Connector"/>
    /// </summary>
    [Name("connector")]
    public class ConnectorConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorConfiguration"/>
        /// </summary>
        /// <param name="ip">The IP address for the connector</param>
        /// <param name="port">The Port to connect to</param>
        /// <param name="unit"><see cref="Unit"/> identifier</param>
        /// <param name="endianness"><see cref="Endianness"/> to expect from the master</param>
        /// <param name="useASCII">Use ASCII transport</param>
        public ConnectorConfiguration(string ip, int port, Unit unit, Endianness endianness, bool useASCII)
        {
            Ip = ip;
            Port = port;
            Unit = unit;
            Endianness = endianness;
            UseASCII = useASCII;
        }

        /// <summary>
        /// Gets the Ip address that will be used for connecting
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Gets the port that will be used for connecting
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Gets the <see cref="Unit"/> identifier
        /// </summary>
        public Unit Unit { get; }

        /// <summary>
        /// Gets the <see cref="Endianness"/> expected from the master
        /// </summary>
        public Endianness Endianness { get;}

        /// <summary>
        /// Gets wether or not to use ASCII transport
        /// </summary>
        /// <value></value>
        public bool UseASCII { get; }
    }
}