using RaaLabs.Edge.Connectors.Modbus.Events;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    class ModbusDatapointOutputVerifier : IProducedEventVerifier<ModbusDatapointOutput>
    {
        public void VerifyFromTableRow(ModbusDatapointOutput @event, TableRow row)
        {
            if (row["Source"].Trim() != "")
            {
                @event.source.Should().Be(row["Source"]);
            }
            if (row["Tag"].Trim() != "")
            {
                @event.tag.Should().Be(row["Tag"]);
            }
            if (row["Value"].Trim() != "")
            {
                if (@event.value is short)
                {
                    short actualValue = @event.value;
                    actualValue.Should().Be(short.Parse(row["Value"]));
                }
                else if (@event.value is uint)
                {
                    uint actualValue = @event.value;
                    actualValue.Should().Be(uint.Parse(row["Value"]));
                }
                else if (@event.value is int)
                {
                    int actualValue = @event.value;
                    actualValue.Should().Be(int.Parse(row["Value"]));
                }
                else if (@event.value is float)
                {
                    float actualValue = @event.value;
                    actualValue.Should().BeApproximately(float.Parse(row["Value"]), 0.0001f);
                }
            }
        }
    }
}
