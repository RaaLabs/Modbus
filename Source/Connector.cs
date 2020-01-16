/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Logging;
using Dolittle.TimeSeries.Modules.Connectors;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Represents a <see cref="IAmAPullConnector">pull connector</see> for Modbus
    /// </summary>
    public class Connector : IAmAPullConnector
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

        public IEnumerable<TagWithData> GetAllData()
        {
            var data = new List<TagWithData>();

            var reverseDatapoints = _configuration.Endianness.ShouldSwapWords();

            foreach (var register in _registers)
            {
                _master.Read(register).ContinueWith(result =>
                {
                    var bytes = result.Result;

                    TagWithData[] tagsWithData = bytes.ToTagsWithData(register, reverseDatapoints);

                    foreach (TagWithData tagWithData in tagsWithData)
                    {
                        _logger.Information($"Tag: {tagWithData.Tag}, Value : {tagWithData.Data}");
                    }

                    data.AddRange(tagsWithData);
                }).Wait();
            }
            return data;
        }

        /// <inheritdoc/>
        public object GetData(Tag tag)
        {
            return new Measurement<Int32> { Value = 0 };
        }
    }
}
