// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaaLabs.Edge.Connectors.Modbus.Model
{
    /// <summary>
    /// The protocol to use for connections
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Straight Modbus TCP
        /// </summary>
        Tcp = 1,

        /// <summary>
        /// Rtu over TCP 
        /// </summary>
        /// <remarks>
        /// More details : https://www.rtautomation.com/technologies/modbus-rtu/
        /// </remarks>
        RtuOverTcp
    }
}