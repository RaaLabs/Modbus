/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;

namespace Dolittle.TimeSeries.Modbus
{
    /// <summary>
    /// Defines a converter for endian
    /// </summary>
    public static class EndiannessExtensions
    {
        /// <summary>
        /// Get correct bytes according to a given <see cref="Endianness"/>
        /// </summary>
        /// <param name="shorts">Array of <see cref="ushort">shorts</see></param>
        /// <param name="endianness"><see cref="Endianness"/> expected in the shorts</param>
        /// <returns>Array of <see cref="byte"/> with correct endian for current CPU</returns>
        public static byte[] GetBytes(this ushort[] shorts, Endianness endianness)
        {
            ushort[] targetShorts = new ushort[shorts.Length];
            Array.Copy(shorts, targetShorts, shorts.Length);

            if (endianness.ShouldSwapWords()) Array.Reverse(targetShorts);

            var bytes = new byte[targetShorts.Length * 2];
            var byteIndex = 0;
            for (var i = 0; i < targetShorts.Length; i++)
            {
                var resultBytes = BitConverter.GetBytes(targetShorts[i]);

                if (endianness.ShouldSwapBytes()) Array.Reverse(resultBytes);

                Array.Copy(resultBytes, 0, bytes, byteIndex, resultBytes.Length);

                byteIndex += resultBytes.Length;
            }

            return bytes;
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
        public static bool ShouldSwapBytes(this Endianness endianness) => endianness == Endianness.HighByteHighWord || endianness == Endianness.HighByteLowWord;
    }
}