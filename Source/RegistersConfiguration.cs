/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.TimeSeries.Modules;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents the configuration for registers
    /// </summary>
    [Name("registers")]
    public class RegistersConfiguration :
        ReadOnlyDictionary<Tag, Register>,
        IConfigurationObject
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RegistersConfiguration"/>
        /// </summary>
        /// <param name="tagsToRegisters">The mapping between all <see cref="Tag">tags</see></param> and <see cref="Register">registers</see>
        public RegistersConfiguration(IDictionary<Tag, Register> tagsToRegisters) : base(tagsToRegisters)
        {
        }
    }
}