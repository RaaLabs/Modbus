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
                @event.Source.Should().Be(row["Source"]);
            }
            if (row["Tag"].Trim() != "")
            {
                @event.Tag.Should().Be(row["Tag"]);
            }
            if (row["Value"].Trim() != "")
            {
                if (@event.Value is short)
                {
                    short actualValue = @event.Value;
                    actualValue.Should().Be(short.Parse(row["Value"]));
                }
                else if (@event.Value is uint)
                {
                    uint actualValue = @event.Value;
                    actualValue.Should().Be(uint.Parse(row["Value"]));
                }
                else if (@event.Value is int)
                {
                    int actualValue = @event.Value;
                    actualValue.Should().Be(int.Parse(row["Value"]));
                }
                else if (@event.Value is float)
                {
                    float actualValue = @event.Value;
                    actualValue.Should().BeApproximately(float.Parse(row["Value"]), 0.0001f);
                }
            }
        }
    }
}
