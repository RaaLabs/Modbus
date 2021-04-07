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
    /// 
    /// </summary>
    public class ModbusRegisterReceivedHandler : IConsumeEvent<ModbusRegisterReceived>, IProduceEvent<ModbusDatapointOutput>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="event"></param>
        public void Handle(ModbusRegisterReceived @event)
        {
            TagWithData[] tagsWithData = @event.Payload.ToTagsWithData(@event.Register);

            var timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            foreach(var tagWithData in tagsWithData)
            {
                if (!tagWithData.Data.Equals(Single.NaN))
                {
                    var output = new ModbusDatapointOutput
                    {
                        source = "Modbus",
                        tag = tagWithData.Tag,
                        timestamp = timestamp,
                        value = tagWithData.Data
                    };

                    SendEvent(output);

                    _logger.Information($"Tag: {tagWithData.Tag}, Value : {tagWithData.Data}");
                }
            }
        }
    }
}
