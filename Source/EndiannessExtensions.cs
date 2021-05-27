// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using RaaLabs.Edge.Connectors.Modbus.Model;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaaLabs.Edge.Connectors.Modbus
{
    /// <summary>
    /// Defines a converter for endian
    /// </summary>
    public static class EndiannessExtensions
    {
        /// <summary>
        /// Extension function to get the bytes of the payload, with corrected endianness.
        /// </summary>
        /// <param name="shorts">the collection of shorts to get the individual bytes from</param>
        /// <param name="endianness">the endianness to use</param>
        /// <param name="dataType">the Modbus data type</param>
        /// <returns>a collection of endianness corrected bytes</returns>
        public static IEnumerable<byte> GetBytes(this IEnumerable<ushort> shorts, Endianness endianness, DataType dataType)
        {

            var shortsInDataPoint = dataType switch
            {
                DataType.Float or DataType.Int32 or DataType.Uint32 => 2,
                _ => 1
            };
            var bytesInDataPoint = shortsInDataPoint * 2;
            var numBytes = shorts.Count() * 2;

            if (numBytes % bytesInDataPoint != 0) throw new ElementsNotPerfectlyDivisableIntoChunksException(numBytes, bytesInDataPoint);

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
        /// <typeparam name="T">the type of the elements in the collection</typeparam>
        /// <param name="elements">elements to reverse chunkwise</param>
        /// <param name="chunkSize">size of chunks</param>
        /// <returns>a chunkwise reversed collection of type T</returns>
        private static IEnumerable<T> ChunkwiseReverse<T>(this IEnumerable<T> elements, int chunkSize)
        {
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

        [Serializable]
        [ExcludeFromCodeCoverage]
        public class ElementsNotPerfectlyDivisableIntoChunksException : Exception
        {
            public int NumElements { get; }
            public int ChunkSize { get; }
            public ElementsNotPerfectlyDivisableIntoChunksException(int numElements, int chunkSize)
                : base($"{numElements} elements not perfectly divisible into chunks of {chunkSize}")
            {
                NumElements = numElements;
                ChunkSize = chunkSize;
            }

            protected ElementsNotPerfectlyDivisableIntoChunksException(SerializationInfo info, StreamingContext context) : base(info, context)
            {

            }

            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
            }
        }
    }
}