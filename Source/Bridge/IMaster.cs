/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Bridge
{
    /// <summary>
    /// Defines a Modbus master
    /// </summary>
    public interface IMaster
    {
        /// <summary>
        /// Read a register from a master
        /// </summary>
        Task<byte[]> Read(Register register);
    }
}