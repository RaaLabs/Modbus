using Machine.Specifications;
using RaaLabs.TimeSeries.Modbus;

namespace RaaLabs.TimeSeries.Modbus.Specifications.for_ByteExtensions
{
    public class when_converting_from_bytes_to_tags_with_data
    {
        Establish context = () =>
        {
            // These 8 bytes consists of either:
            // - two 32 bit floats, 1234.56 and 133.3, encoded with little endian
            // - two 32 bit integers, 0x449a51ec and 0x43054ccd, encoded with little endian
            // - four sequential 16 bit integers, 0x51ec, 0x449a, 0x4ccd, 0x4305, encoded with little endian
            input = new byte[] { 0xec, 0x51, 0x9a, 0x44, 0xcd, 0x4c, 0x05, 0x43 };

            twoFloats = new Register();
            twoFloats.Unit = 1;
            twoFloats.StartingAddress = 1;
            twoFloats.DataType = DataType.Float;
            twoFloats.FunctionCode = FunctionCode.HoldingRegister;
            twoFloats.Size = 2;

            twoInt32s = new Register();
            twoInt32s.Unit = 1;
            twoInt32s.StartingAddress = 1;
            twoInt32s.DataType = DataType.Int32;
            twoInt32s.FunctionCode = FunctionCode.HoldingRegister;
            twoInt32s.Size = 2;

            fourInt16s = new Register();
            fourInt16s.Unit = 1;
            fourInt16s.StartingAddress = 1;
            fourInt16s.DataType = DataType.Int16;
            fourInt16s.FunctionCode = FunctionCode.HoldingRegister;
            fourInt16s.Size = 4;
        };

        Because of = () =>
        {
            twoFloatTags = input.ToTagsWithData(twoFloats, false);
            twoInt32Tags = input.ToTagsWithData(twoInt32s, false);
            fourInt16Tags = input.ToTagsWithData(fourInt16s, false);
        };

        It should_have_the_correct_addresses_and_values_for_twoFloatTags = () =>
        {
            twoFloatTags[0].Tag.ShouldEqual<Tag>("1:1");
            twoFloatTags[1].Tag.ShouldEqual<Tag>("1:3");

            twoFloatTags[0].Data.ShouldEqual(1234.56f);
            twoFloatTags[1].Data.ShouldEqual(133.3f);
        };

        It should_have_the_correct_addresses_and_values_for_twoInt32Tags = () =>
        {
            twoInt32Tags[0].Tag.ShouldEqual<Tag>("1:1");
            twoInt32Tags[1].Tag.ShouldEqual<Tag>("1:3");

            twoInt32Tags[0].Data.ShouldEqual(0x449a51ec);
            twoInt32Tags[1].Data.ShouldEqual(0x43054ccd);
        };

        It should_have_the_correct_addresses_and_values_for_fourInt16Tags = () =>
        {
            fourInt16Tags[0].Tag.ShouldEqual<Tag>("1:1");
            fourInt16Tags[1].Tag.ShouldEqual<Tag>("1:2");
            fourInt16Tags[2].Tag.ShouldEqual<Tag>("1:3");
            fourInt16Tags[3].Tag.ShouldEqual<Tag>("1:4");

            fourInt16Tags[0].Data.ShouldEqual((short)0x51ec);
            fourInt16Tags[1].Data.ShouldEqual((short)0x449a);
            fourInt16Tags[2].Data.ShouldEqual((short)0x4ccd);
            fourInt16Tags[3].Data.ShouldEqual((short)0x4305);
        };

        static byte[] input;
        static Register twoFloats;
        static Register twoInt32s;
        static Register fourInt16s;

        static TagWithData[] twoFloatTags;
        static TagWithData[] twoInt32Tags;
        static TagWithData[] fourInt16Tags;
    }
}
