using RaaLabs.Edge.Connectors.Modbus.Bridge;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;
using RaaLabs.Edge.Connectors.Modbus.Specs.Drivers;
using RaaLabs.Edge.Connectors.Modbus;
using System.Linq;
using System;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Steps
{
    [Binding]
    class EndiannessExtensionsSteps
    {
        private List<(DataType dataType, Endianness endianness, ushort[] data)> _shorts = new List<(DataType, Endianness, ushort[])>();
        private List<byte[]> _bytes;

        [Given(@"the following shorts")]
        void GivenTheFollowingShorts(Table table)
        {
            foreach(var row in table.Rows)
            {
                Endianness endianness = Enum.Parse<Endianness>(row["Endianness"]);
                DataType dataType = Enum.Parse<DataType>(row["DataType"]);
                var contentBytes = row["Content"].ToBytes();
                var contentShorts = Enumerable.Range(0, contentBytes.Length / 2).Select(i => BitConverter.ToUInt16(contentBytes, i * 2)).ToArray();
                _shorts.Add((dataType, endianness, contentShorts));
            }
        }

        [When(@"converting the shorts to bytes")]
        void WhenConvertingTheShortsToBytes()
        {
            _bytes = _shorts.Select(sh => sh.data.GetBytes(sh.endianness, sh.dataType).ToArray()).ToList();
        }

        [Then(@"the shorts will be converted to the following bytes")]
        void ThenTheShortsWillBeConvertedToTheFollowingBytes(Table table)
        {
            var expectedBytes = table.Rows.Select(row => row["Content"].ToBytes()).ToList();
            _bytes.Should().BeEquivalentTo(expectedBytes);
        }
    }
}
