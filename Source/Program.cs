/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using RaaLabs.TimeSeries.Modules.Booting;

namespace RaaLabs.TimeSeries.Modbus
{
    class Program
    {
        static void Main()
        {
            Bootloader.Configure(_ => { }).Start().Wait();
        }
    }
}