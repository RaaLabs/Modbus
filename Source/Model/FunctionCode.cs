/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace RaaLabs.Edge.Connectors.Modbus.Model
{
    /// <summary>
    /// Represents a Modbus function code
    /// </summary>
    /// <remarks>
    /// http://www.simplymodbus.ca/FAQ.htm#FC
    /// </remarks>
    public enum FunctionCode
    {
        /// <summary>
        /// Discrete output coil
        /// </summary>
        Coil = 1,

        /// <summary>
        /// Holding registers
        /// </summary>
        HoldingRegister,

        /// <summary>
        /// Discrete input contacts
        /// </summary>
        Input,

        /// <summary>
        /// Input register
        /// </summary>
        InputRegister
    }
}