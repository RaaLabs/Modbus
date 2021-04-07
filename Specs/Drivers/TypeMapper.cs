using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaaLabs.Edge.Connectors.Modbus;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    public class TypeMapping : Dictionary<string, Type>
    {
        public TypeMapping()
        {
            Add("ModbusRegisterReceivedHandler", typeof(ModbusRegisterReceivedHandler));
            Add("ModbusRegisterReceived", typeof(Events.ModbusRegisterReceived));
            Add("ModbusDatapointOutput", typeof(Events.ModbusDatapointOutput));
        }
    }
}
