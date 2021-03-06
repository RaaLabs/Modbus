// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Connectors.Modbus.Events;
using RaaLabs.Edge.Modules.EventHandling;
using System;

namespace RaaLabs.Edge.Connectors.Modbus
{
    /// <summary>
    /// Handler for parsing Modbus data points and sending these data points to EdgeHub
    /// </summary>
    public class ModbusRegisterReceivedHandler : IConsumeEvent<ModbusRegisterReceived>, IProduceEvent<ModbusDatapointOutput>
    {
        /// <summary>
        /// Delegate for sending event to EdgeHub
        /// </summary>
        public event EventEmitter<ModbusDatapointOutput> SendEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ModbusRegisterReceivedHandler()
        {
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
                        Source = "Modbus",
                        Tag = tagWithData.tag,
                        Timestamp = timestamp,
                        Value = tagWithData.data
                    };

                    SendEvent(output);
                }
            }
        }
    }
}
