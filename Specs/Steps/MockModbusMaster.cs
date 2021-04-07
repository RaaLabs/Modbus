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
    public sealed class MockModbusMaster
    {
        private readonly IObjectContainer _container;

        public MockModbusMaster(IObjectContainer container)
        {
            _container = container;
        }

        [Given(@"a Modbus Register with the following contents")]
        public void GivenAModbusRegisterWithTheFollowingContents(Table table)
        {
            var master = new Mock<IMaster>();
            var requestedRegisters = table.Rows.Select(row => row.CreateInstance<RequestedRegister>()).ToList();

            // Mock all registers in IMaster
            requestedRegisters.ForEach(register => master.Setup(m => m.Read(It.Is<Register>(r => register.Satisfies(r)))).Returns(Task.FromResult(register.Content.ToBytes())));

            _container.RegisterInstanceAs(master.Object);

            var registerContents = new RegisterContents(requestedRegisters);
            _container.RegisterInstanceAs(registerContents);
        }
    }
}
