using RaaLabs.Edge.Connectors.Modbus.Events;
using RaaLabs.Edge.Modules.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus
{
    /// <summary>
    /// Handler for parsing Modbus data points and sending these data points to EdgeHub
    /// </summary>
    public class ModbusRegisterReceivedHandler : IConsumeEvent<ModbusRegisterReceived>, IProduceEvent<ModbusDatapointOutput>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Delegate for sending event to EdgeHub
        /// </summary>
        public event EventEmitter<ModbusDatapointOutput> SendEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ModbusRegisterReceivedHandler(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handler for incoming modbus data point
        /// </summary>
        /// <param name="event"></param>
        public void Handle(ModbusRegisterReceived @event)
        {
            var tagsWithData = @event.Payload.ExtractDataPoints(@event.Register);

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            foreach(var tagWithData in tagsWithData)
            {
                if (!tagWithData.data.Equals(Single.NaN))
                {
                    var output = new ModbusDatapointOutput
                    {
                        source = "Modbus",
                        tag = tagWithData.tag,
                        timestamp = timestamp,
                        value = tagWithData.data
                    };

                    SendEvent(output);
                }
            }
        }
    }
}
