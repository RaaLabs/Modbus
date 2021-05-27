// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using RaaLabs.Edge.Modules.EventHandling;
using System.Diagnostics;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Bridge
{
    /// <summary>
    /// 
    /// </summary>
    public class ModbusBridge : IRunAsync, IProduceEvent<Events.ModbusRegisterReceived>
    {
        private readonly ILogger _logger;
        private readonly RegistersConfiguration _registers;
        private readonly ConnectorConfiguration _configuration;
        private readonly IMaster _master;

        /// <summary>
        /// 
        /// </summary>
        public event EventEmitter<Events.ModbusRegisterReceived> ModbusRegisterReceived;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="master"></param>
        /// <param name="registers"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public ModbusBridge(IMaster master, RegistersConfiguration registers, ConnectorConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _master = master;
            _registers = registers;
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            _logger.Information("Running!");
            await Connect();
        }

        /// <inheritdoc/>
        public async Task Connect()
        {
            var interval = _configuration.Interval;

            while (true)
            {
                try
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    IEnumerable<Task<(Register, byte[])>> readTasks = _registers
                        .Select(async r => (r, await _master.Read(r)));

                    var results = await Task.WhenAll(readTasks);

                    results.ToList().ForEach(r => ModbusRegisterReceived(r));
                    
                    timer.Stop();

                    // If interval is less than zero, this means that modbus register polling should only occur once.
                    if (interval < 0)
                    {
                        break;
                    }

                    int elapsed = (int)timer.ElapsedMilliseconds;
                    if (elapsed < interval)
                    {
                        await Task.Delay(interval - elapsed);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error while connecting to Modbus stream");
                    await Task.Delay(2000);
                }
            }
        }
    }
}
