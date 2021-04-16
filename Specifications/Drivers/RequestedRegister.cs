using RaaLabs.Edge.Connectors.Modbus.Model;

namespace RaaLabs.Edge.Connectors.Modbus.Specs.Drivers
{
    class RequestedRegister
    {
        public RequestedRegister(byte? unit, ushort? startingAddress, DataType? dataType, FunctionCode? functionCode, ushort? size, string content)
        {
            Unit = unit;
            StartingAddress = startingAddress;
            DataType = dataType;
            FunctionCode = functionCode;
            Size = size;
            Content = content;
        }

        public bool Satisfies(Register register)
        {
            bool unitMatch = Unit != null ? Unit == register.Unit : true;
            bool startingAddressMatch = StartingAddress != null ? StartingAddress == register.StartingAddress : true;
            bool dataTypeMatch = DataType != null ? DataType == register.DataType : true;
            bool functionCodeMatch = FunctionCode != null ? FunctionCode == register.FunctionCode : true;
            bool sizeMatch = Size != null ? Size == register.Size : true;

            return unitMatch && startingAddressMatch && dataTypeMatch && functionCodeMatch && sizeMatch;
        }

        public byte? Unit { get; set; }
        public ushort? StartingAddress { get; set; }
        public DataType? DataType { get; set; }
        public FunctionCode? FunctionCode { get; set; }
        public ushort? Size { get; set; }
        public string Content { get; set; }
    }
}
