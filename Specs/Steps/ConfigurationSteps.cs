using BoDi;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using RaaLabs.Edge.Connectors.Modbus.Bridge;
using RaaLabs.Edge.Connectors.Modbus.Model;
using RaaLabs.Edge.Connectors.Modbus.Specs.Drivers;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Steps
{
    [Binding]
    public sealed class ConfigurationSteps
    {
        private readonly IObjectContainer _container;

        public ConfigurationSteps(IObjectContainer container)
        {
            _container = container;
        }

        [Given(@"a register configuration with the following values")]
        public void GivenTheFollowingRegisterConfiguration(Table table)
        {
            var registers = table.Rows.Select(row => row.CreateInstance<Register>()).ToList();

            _container.RegisterInstanceAs(new RegistersConfiguration(registers));
        }

        [Given(@"a connector configuration with the following values")]
        public void GivenAConnectorConfigurationWithTheFollowingValues(Table table)
        {
            ConnectorConfiguration configuration = table.CreateInstance<ConnectorConfiguration>();
            _container.RegisterInstanceAs(configuration);
        }
    }
}
