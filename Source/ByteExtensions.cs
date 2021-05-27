// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus
{
    /// <summary>
    /// Defines extension functions to interpret bytes
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Extract tags and data from byte array/>
        /// </summary>
        /// <param name="bytes">Array of <see cref="byte">. Expected to be little endian.</see></param>
        /// <param name="register">Register information <see cref="Register">register</see></param>
        /// <returns>a collection of tuples containing tag and data</returns>
        public static IEnumerable<(string tag, dynamic data)> ExtractDataPoints(this byte[] bytes, Register register)
        {
            var datapointSize = GetDatapointSizeFrom(register.DataType);

            var convertedDataPoints = bytes.Chunk(datapointSize).Select(data => ConvertBytes(register.DataType, data.ToArray()));
            var tagsWithData = convertedDataPoints.Select((payload, index) =>
            {
                var tag = $"{register.Unit}:{register.StartingAddress + index * (datapointSize / 2)}";
                return (tag, payload);
            });

            return tagsWithData;
        }

        static ushort GetDatapointSizeFrom(DataType type)
        {
            return type switch
            {
                DataType.Int32 => 4,
                DataType.Uint32 => 4,
                DataType.Float => 4,
                _ => 2
            };
        }

        static dynamic ConvertBytes(DataType type, byte[] bytes)
        {
            return type switch
            {
                DataType.Int32 => BitConverter.ToInt32(bytes),
                DataType.Uint32 => BitConverter.ToUInt32(bytes),
                DataType.Float => BitConverter.ToSingle(bytes),
                _ => BitConverter.ToInt16(bytes),
            };
        }

        /// <summary>
        /// Extension function to separate a collection into chunks of a given size
        /// </summary>
        /// <typeparam name="T">the type in the collection</typeparam>
        /// <param name="source">the collection to separate into chunks</param>
        /// <param name="size">the chunk size</param>
        /// <returns>a collection of chunks, each of the given size</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            while (source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
            }
        }
    }
}
