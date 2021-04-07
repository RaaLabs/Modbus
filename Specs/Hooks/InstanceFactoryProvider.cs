using BoDi;
using RaaLabs.Edge.Connectors.Modbus.Events;
using RaaLabs.Edge.Connectors.Modbus.Specs.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Hooks
{
    [Binding]
    class InstanceFactoryProvider
    {
        private readonly IObjectContainer _container;

        public InstanceFactoryProvider(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupLogger()
        {
            _container.RegisterTypeAs<ModbusRegisterReceivedInstanceFactory, IEventInstanceFactory<ModbusRegisterReceived>>();
            _container.RegisterTypeAs<ModbusDatapointOutputVerifier, IProducedEventVerifier<ModbusDatapointOutput>>();
        }

    }
}
