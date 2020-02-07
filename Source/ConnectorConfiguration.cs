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
    [Name("connector")]
    public class ConnectorConfiguration : IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorConfiguration"/>
        /// </summary>
        /// <param name="ip">The IP address for the connector</param>
        /// <param name="port">The Port to connect to</param>
        /// <param name="endianness"><see cref="Endianness"/> to expect from the master</param>
        /// <param name="protocol"><see cref="Protocol"/> to use for connecting</param>
        /// <param name="useASCII">Use ASCII transport</param>
        /// <param name="interval">Use ASCII transport</param>
        /// <param name="timeout"> The read timeout for the connector</param>
        public ConnectorConfiguration(string ip, int port, Endianness endianness, Protocol protocol, bool useASCII, int interval, int timeout)
        {
            Ip = ip;
            Port = port;
            Endianness = endianness;
            Protocol = protocol;
            UseASCII = useASCII;
            Interval = interval;
            Timeout = timeout;
        }

        /// <summary>
        /// Gets the Ip address that will be used for connecting
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Gets the port that will be used for connecting
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Gets the <see cref="Endianness"/> expected from the master
        /// </summary>
        public Endianness Endianness { get; }

        /// <summary>
        /// Gets the <see cref="Protocol"/> to use
        /// </summary>
        public Protocol Protocol { get; }

        /// <summary>
        /// Gets wether or not to use ASCII transport
        /// </summary>
        /// <value></value>
        public bool UseASCII { get; }

        /// <summary>
        /// Gets the poll interval for the connector
        /// </summary>
        /// <value></value>
        public int Interval { get; }

        /// <summary>
        /// set the read timeout for the connector
        /// </summary>
        /// <value></value>
        public int Timeout { get; }
    }
}