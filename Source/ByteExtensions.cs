/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
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
        public static IEnumerable<(string tag, object data)> ExtractDataPoints(this byte[] bytes, Register register)
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
