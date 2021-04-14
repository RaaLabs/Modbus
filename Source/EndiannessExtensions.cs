/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using RaaLabs.Edge.Connectors.Modbus.Model;
using System.Linq;
using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.Modbus
{
    /// <summary>
    /// Defines a converter for endian
    /// </summary>
    public static class EndiannessExtensions
    {
        /// <summary>
        /// Gets the bytes of the payload, with corrected endianness.
        /// </summary>
        /// <param name="shorts"></param>
        /// <param name="endianness"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static IEnumerable<byte> GetBytes(this IEnumerable<ushort> shorts, Endianness endianness, DataType dataType)
        {
            var shortsInDataPoint = dataType switch
            {
                DataType.Float or DataType.Int32 or DataType.Uint32 => 2,
                _ => 1
            };
            var endian = BitConverter.IsLittleEndian;

            shorts = (endianness.ShouldSwapWords()) ? shorts.ChunkwiseReverse(shortsInDataPoint) : shorts;
            var bytes = shorts.SelectMany(sh => BitConverter.GetBytes(sh)).ToList();
            bytes = (endianness.ShouldSwapBytesInWords()) ? bytes.ChunkwiseReverse(2).ToList() : bytes;

            return bytes;
        }

        /// <summary>
        /// Reverse every chunk of N elements within the collection.
        /// 
        /// Example:
        /// 
        /// Let's reverse each chunk of 3 elements in the following element collection:
        /// ABC DEF GHI
        /// 
        /// This will yield the following new collection:
        /// CBA FED IHG
        /// 
        /// A chunk size of 1 will yield a copy of the original collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        private static IEnumerable<T> ChunkwiseReverse<T>(this IEnumerable<T> elements, int chunkSize)
        {
            if (elements.Count() % chunkSize != 0) throw new Exception("Elements must perfectly dividable by chunk size");

            while (elements.Any())
            {
                var reversedChunk = elements.Take(chunkSize).Reverse();
                foreach (var element in reversedChunk)
                {
                    yield return element;
                }
                elements = elements.Skip(chunkSize);
            }
        }

        /// <summary>
        /// Check if words (16 bit) should be swapped within typically a double word (32 bit)
        /// </summary>
        /// <param name="endianness"><see cref="Endianness"/> to check</param>
        /// <returns>True if it should be swapped, false if not</returns>
        public static bool ShouldSwapWords(this Endianness endianness) => endianness == Endianness.HighByteHighWord || endianness == Endianness.LowByteHighWord;

        /// <summary>
        /// Check if bytes (8 bit) should be swapped within typically a word (16 bit)
        /// </summary>
        /// <param name="endianness"><see cref="Endianness"/> to check</param>
        /// <returns>True if it should be swapped, false if not</returns>
        public static bool ShouldSwapBytesInWords(this Endianness endianness) => endianness == Endianness.HighByteHighWord || endianness == Endianness.HighByteLowWord;
    }
}