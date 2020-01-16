/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Concepts;

namespace RaaLabs.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a Modbus unit
    /// </summary>
    public class Unit : ConceptAs<byte>
    {
        /// <summary>
        /// Implicitly convert from <see cref="byte"/> to <see cref="Unit"/>
        /// </summary>
        /// <param name="unit">Byte representation to convert from</param>
        public static implicit operator Unit(byte unit)
        {
            return new Unit { Value = unit };
        }
    }
}