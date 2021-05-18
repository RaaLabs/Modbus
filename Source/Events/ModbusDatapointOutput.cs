using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Connectors.Modbus.Events
{
    /// <summary>
    /// Implement outgoing event
    /// </summary>
    [OutputName("output")]
    public class ModbusDatapointOutput : IEdgeHubOutgoingEvent
    {
        /// <summary>
        /// Represents the source system.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the tag. Represens the sensor name from the source system.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public dynamic Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in the form of EPOCH milliseconds granularity
        /// </summary>
        public long Timestamp { get; set; }
    }
}
