/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a Modbus register
    /// </summary>
    public class Register : Value<Register>
    {
        /// <summary>
        /// Gets the <see cref="Unit"/> identifier
        /// </summary>
        public Unit Unit { get; set; }

        /// <summary>
        /// Gets or sets the starting address for the register
        /// </summary>
        /// <remarks>
        /// This is the actual zero-indexed short/word (16 bit) offset.
        /// 0: 0 bytes
        /// 1: 2 bytes 
        /// 2: 4 bytes 
        /// ...
        /// </remarks>
        public ushort StartingAddress { get; set; }

        /// <summary>
        /// Gets or sets what <see cref="DataType"/> to expect for the register
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// The <see cref="FunctionCode"/> to use for the read
        /// </summary>
        public FunctionCode FunctionCode { get; set; }
    }
}