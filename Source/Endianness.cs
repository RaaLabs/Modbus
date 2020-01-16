/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace RaaLabs.TimeSeries.Modbus
{
    /// <summary>
    /// The endian to expect from the data
    /// </summary>
    /// <remarks>
    /// http://www.simplymodbus.ca/FAQ.htm#Order
    ///
    /// Modbus is not strict on endian on its data, its the implementor that decides
    /// These are the variations typically seen
    ///  
    /// AE41 5652       high byte first      high word first    "big endian"
    /// 5652 AE41       high byte first      low word first
    /// 41AE 5256       low byte first      high word first
    /// 5256 41AE       low byte first      low word first     "little endian"
    /// </remarks>
    public enum Endianness
    {
        /// <summary>
        /// High byte first, high word first
        /// </summary>
        HighByteHighWord=1,

        /// <summary>
        /// High byte first, low word first
        /// </summary>
        HighByteLowWord,

        /// <summary>
        /// Low byte first, high word first
        /// </summary>
        LowByteHighWord,

        /// <summary>
        /// Low byte first, low word first
        /// </summary>
        LowByteLowWord,
    }
}