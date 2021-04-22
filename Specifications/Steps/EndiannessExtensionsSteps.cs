using System.Collections.Generic;
using TechTalk.SpecFlow;
using FluentAssertions;
using RaaLabs.Edge.Connectors.Modbus.Specs.Drivers;
using System.Linq;
using System;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Steps
{
    [Binding]
    class EndiannessExtensionsSteps
    {
        private List<(int index, DataType dataType, Endianness endianness, ushort[] data)> _shorts = new List<(int, DataType, Endianness, ushort[])>();
        private List<byte[]> _bytes;
        private List<(int index, Exception exception)> _exceptions;

        [Given(@"the following shorts")]
        void GivenTheFollowingShorts(Table table)
        {
            foreach (var (row, index) in table.Rows.Select((item, index) => (item, index + 1)))
            {
                Endianness endianness = Enum.Parse<Endianness>(row["Endianness"]);
                DataType dataType = Enum.Parse<DataType>(row["DataType"]);
                var contentBytes = row["Content"].ToBytes();
                var contentShorts = Enumerable.Range(0, contentBytes.Length / 2).Select(i => BitConverter.ToUInt16(contentBytes, i * 2)).ToArray();
                _shorts.Add((index, dataType, endianness, contentShorts));
            }
        }

        [When(@"converting the shorts to bytes")]
        void WhenConvertingTheShortsToBytes()
        {
            _bytes = new List<byte[]>();
            _exceptions = new List<(int, Exception)>();

            foreach (var sh in _shorts)
            {
                try
                {
                    _bytes.Add(sh.data.GetBytes(sh.endianness, sh.dataType).ToArray());
                }
                catch (Exception e)
                {
                    _exceptions.Add((sh.index, e));
                }
            }

        }

        [Then(@"the shorts will be converted to the following bytes")]
        void ThenTheShortsWillBeConvertedToTheFollowingBytes(Table table)
        {
            var expectedBytes = table.Rows.Select(row => row["Content"].ToBytes()).ToList();
            _bytes.Should().BeEquivalentTo(expectedBytes);
        }

        [Then(@"the following input rows should throw an exception")]
        void ThenTheFollowingInputRowsShouldThrowAnException(Table table)
        {
            _exceptions.Count.Should().Be(table.RowCount);
            var expectedRows = table.Rows.Select(r => int.Parse(r["RowNumber"])).ToList();
            var both = expectedRows.Zip(_exceptions.Select(e => e.index)).ToList();
            foreach (var (expected, actual) in both)
            {
                expected.Should().Be(actual);
            }
        }
    }
}
