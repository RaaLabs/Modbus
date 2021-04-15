using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Connectors.Modbus.Events
{
    /// <summary>
    /// 
    /// </summary>
    [OutputName("output")]
    public class ModbusDatapointOutput : IEdgeHubOutgoingEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public dynamic value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long timestamp { get; set; }
    }
}
