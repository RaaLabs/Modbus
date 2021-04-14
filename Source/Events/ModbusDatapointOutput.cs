using RaaLabs.Edge.Modules.EdgeHub;
using System.Diagnostics.CodeAnalysis;

namespace RaaLabs.Edge.Connectors.Modbus.Events
{
    /// <summary>
    /// 
    /// </summary>
    [OutputName("output")]
    [ExcludeFromCodeCoverage]
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