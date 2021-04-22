using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    /// <summary>
    /// 
    /// </summary>
    class RegisterContents : List<RequestedRegister>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents"></param>
        public RegisterContents(List<RequestedRegister> contents) : base(contents)
        {

        }
    }
}
