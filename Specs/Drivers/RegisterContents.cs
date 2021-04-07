using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
