/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using RaaLabs.Edge;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Connectors.Modbus.Bridge;
using RaaLabs.Edge.Connectors.Modbus;
using System.Diagnostics.CodeAnalysis;

namespace RaaLabs.TimeSeries.Modbus
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main()
        {
            var application = new ApplicationBuilder()
                .WithModule<EventHandling>()
                .WithModule<Configuration>()
                .WithModule<EdgeHub>()
                .WithTask<ModbusBridge>()
                .WithHandler<ModbusRegisterReceivedHandler>()
                .WithType<Master>()
                .Build();

            application.Run().Wait();
        }
    }
}