using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Serilog;
using BoDi;
using RaaLabs.Edge.Testing;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Hooks
{
    [Binding]
    public sealed class TypeMapperProvider
    {
        private readonly TypeMapping _typeMapping;

        public TypeMapperProvider(TypeMapping typeMapping)
        {
            _typeMapping = typeMapping;
        }

        [BeforeScenario]
        public void SetupTypes()
        {
            _typeMapping.Add("ModbusRegisterReceivedHandler", typeof(ModbusRegisterReceivedHandler));
            _typeMapping.Add("ModbusRegisterReceived", typeof(Events.ModbusRegisterReceived));
            _typeMapping.Add("ModbusDatapointOutput", typeof(Events.ModbusDatapointOutput));
        }
    }
}
