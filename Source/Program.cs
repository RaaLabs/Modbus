/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.TimeSeries.Modules.Booting;

namespace Dolittle.TimeSeries.Modbus
{
    class Program
    {
        static void Main()
        {
            Bootloader.Configure(_ => {}).Start().Wait();
        }
    }
}