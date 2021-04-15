using RaaLabs.Edge.Connectors.Modbus.Events;
using RaaLabs.Edge.Connectors.Modbus.Model;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    class ModbusRegisterReceivedInstanceFactory : IEventInstanceFactory<ModbusRegisterReceived>
    {
        public ModbusRegisterReceived FromTableRow(TableRow row)
        {
            var register = row.CreateInstance<Register>();
            var contents = row["Content"].ToBytes();
            return new ModbusRegisterReceived(register, contents);
        }
    }
}
