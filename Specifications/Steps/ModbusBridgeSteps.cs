using RaaLabs.Edge.Connectors.Modbus.Bridge;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;
using RaaLabs.Edge.Connectors.Modbus.Specs.Drivers;
using System.Linq;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Steps
{
    [Binding]
    class ModbusBridgeSteps
    {
        private ModbusBridge _bridge;
        private List<Events.ModbusRegisterReceived> _eventsReceived = new List<Events.ModbusRegisterReceived>();

        public ModbusBridgeSteps(ModbusBridge bridge)
        {
            _bridge = bridge;
            _bridge.ModbusRegisterReceived += (ev) =>
            {
                _eventsReceived.Add(ev);
            };
        }

        [When(@"establishing connection to modbus")]
        public void WhenEstablishingConnectionToModbus()
        {
            _bridge.Connect().Wait();
        }

        [Then(@"the following ModbusRegisterReceived events should be emitted")]
        public void ThenTheFollowingRegistersShouldBeRead(Table table)
        {
            table.Rows.Count.Should().Be(_eventsReceived.Count);
            foreach(var row in table.Rows)
            {
                var register = row.CreateInstance<RequestedRegister>();
                var @event = _eventsReceived.Where(ev => register.Satisfies(ev.Register)).First();
                var expectedBytes = register.Content.ToBytes();
                var receivedBytes = @event.Payload;
                expectedBytes.Should().BeEquivalentTo(receivedBytes);
            }
        }
    }
}
