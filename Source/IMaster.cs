/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Defines a Modbus master
    /// </summary>
    public interface IMaster
    {
        /// <summary>
        /// Read a register from a master
        /// </summary>
        byte[] Read(Register register);
    }
}