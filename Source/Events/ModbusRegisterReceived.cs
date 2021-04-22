using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ModbusRegisterReceived : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public Register Register { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <param name="payload"></param>
        public ModbusRegisterReceived(Register register, byte[] payload)
        {
            Register = register;
            Payload = payload;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ModbusRegisterReceived((Register register, byte[] payload) value) => new ModbusRegisterReceived(value.register, value.payload);
    }
}
