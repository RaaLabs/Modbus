/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Modbus.Specifications.for_EndianessExtensions.when_getting_bytes_from_two_shorts
{
    public class and_endian_is_high_byte_and_low_word_first
    {
        static ushort[] shorts;
        static byte[] result;

        Establish context = () =>
        {
            shorts = new []
            {
                BitConverter.ToUInt16(new byte[] { 0x56, 0x52 }),
                BitConverter.ToUInt16(new byte[] { 0xae, 0x41 })
                
            };
        };

        Because of = () => result = shorts.GetBytes(Endianness.HighByteLowWord);

        It should_convert_correctly_to_little_endian = () => result.ShouldEqual(new byte[] {  0x52, 0x56, 0x41, 0xae });
    }
}