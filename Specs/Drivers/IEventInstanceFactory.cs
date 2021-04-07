using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    interface IEventInstanceFactory<T>
    {
        public T FromTableRow(TableRow row);
    }
}
