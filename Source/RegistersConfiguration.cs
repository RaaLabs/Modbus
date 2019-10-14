/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents the configuration for registers
    /// </summary>
    [Name("registers")]
    public class RegistersConfiguration :
        ReadOnlyCollection<Register>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RegistersConfiguration"/>
        /// </summary>
        /// <param name="registers">The mapping between all </param> and <see cref="Register">registers</see>
        public RegistersConfiguration(IList<Register> registers) : base(registers)
        {
        }
    }
}