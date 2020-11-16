/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaaLabs.TimeSeries.Modbus
{
    /// <summary>
    /// Defines extension functions to interpret bytes
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Convert from bytes to array of <see cref="TagWithData"/>
        /// </summary>
        /// <param name="bytes">Array of <see cref="byte">. Expected to be little endian.</see></param>
        /// <param name="register">Register information <see cref="Register">register</see></param>
        /// <param name="reverseDatapoints">Is set if the data points should come in reversed order</param>
        /// <returns>Array of <see cref="TagWithData"/></returns>
        public static TagWithData[] ToTagsWithData(this byte[] bytes, Register register, bool reverseDatapoints = false)
        {
            var datapointSize = GetDatapointSizeFrom(register.DataType);

            if (reverseDatapoints)
            {
                var tempBytes = new List<byte>();
                for (var byteIndex = bytes.Length; byteIndex >= 0; byteIndex -= datapointSize)
                {
                    tempBytes.AddRange(bytes.Skip(byteIndex).Take(datapointSize).ToArray());
                }
                bytes = tempBytes.ToArray();
            }
            var convertedDataPoints = bytes.Chunk(datapointSize).Select(data => ConvertBytes(register.DataType, data.ToArray()));
            var tagsWithData = convertedDataPoints.Select((payload, index) =>
            {
                var tag = $"{register.Unit}:{register.StartingAddress + index * (datapointSize / 2)}";
                return new TagWithData(tag, payload);
            });

            return tagsWithData.ToArray();
        }

        static ushort GetDatapointSizeFrom(DataType type)
        {
            switch (type)
            {
                case DataType.Int32:
                    return 4;
                case DataType.Uint32:
                    return 4;
                case DataType.Float:
                    return 4;
                case DataType.Int16:
                    return 2;
            }
            return 2;
        }

        static object ConvertBytes(DataType type, byte[] bytes)
        {
            switch (type)
            {
                case DataType.Int32:
                    return BitConverter.ToInt32(bytes);
                case DataType.Uint32:
                    return BitConverter.ToUInt32(bytes);
                case DataType.Float:
                    return BitConverter.ToSingle(bytes);
                case DataType.Int16:
                    return BitConverter.ToInt16(bytes);
            }
            return 0;
        }

        private static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            while (source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
            }
        }
    }
}
