/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Dolittle.Logging;
using RaaLabs.TimeSeries.Modules;
using RaaLabs.TimeSeries.Modules.Connectors;

namespace RaaLabs.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a <see cref="IAmAStreamingConnector">streaming connector</see> for Modbus
    /// </summary>
    public class Connector : IAmAStreamingConnector
    {
        readonly RegistersConfiguration _registers;
        readonly ConnectorConfiguration _configuration;
        readonly IMaster _master;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="registers">The <see cref="RegistersConfiguration">configured registers</see></param>
        /// <param name="configuration"><see cref="ConnectorConfiguration">Configuration</see></param>
        /// <param name="master">The <see cref="IMaster"/></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            RegistersConfiguration registers,
            ConnectorConfiguration configuration,
            IMaster master,
            ILogger logger)
        {
            _registers = registers;
            _configuration = configuration;
            _logger = logger;
            _master = master;
        }


        /// <inheritdoc/>
        public Source Name => "Modbus";

        /// <inheritdoc/>
        public event DataReceived DataReceived = (tag, ValueTask, timestamp) => { };

        /// <inheritdoc/>
        public void Connect()
        {
            var reverseDatapoints = _configuration.Endianness.ShouldSwapWords();
            var interval = _configuration.Interval;

            while (true)
            {
                var timer = new Stopwatch();
                timer.Start();
                foreach (var register in _registers)
                {
                    _master.Read(register).ContinueWith(result =>
                    {
                        var bytes = result.Result;

                        TagWithData[] tagsWithData = bytes.ToTagsWithData(register, reverseDatapoints);

                        foreach (TagWithData tagWithData in tagsWithData)
                        {
                            DataReceived(tagWithData.Tag, tagWithData.Data, Timestamp.UtcNow);
                            _logger.Information($"Tag: {tagWithData.Tag}, Value : {tagWithData.Data}");
                        }

                    }).Wait();
                }

                timer.Stop();
                int elapsed = (int)timer.ElapsedMilliseconds;
                if (elapsed < interval)
                {
                    Task.Delay(interval - elapsed).Wait();
                }
            }
        }
    }
}
