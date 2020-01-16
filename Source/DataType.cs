/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace RaaLabs.TimeSeries.Modbus
{
    /// <summary>
    /// Represents the different types of data a <see cref="Register"/> can have
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 32 bit integer
        /// </summary>
        Int32=1,

        /// <summary>
        /// Unsigned 32 bit integer
        /// </summary>
        Uint32,

        /// <summary>
        /// IEEE 754 floating point; https://en.m.wikipedia.org/wiki/IEEE_754
        /// </summary>
        Float,

        /// <summary>
        /// 16 bit integer
        /// </summary>
        Int16
    }
}