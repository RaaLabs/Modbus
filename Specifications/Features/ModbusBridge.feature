Feature: ModbusBridge

Background:
	Given a Modbus Register with the following contents
		| Unit | StartingAddress | DataType | FunctionCode | Size | Content              |
		| 1    | 1               | 4        | 1            | 1    | ABCD                 |
		| 1    | 2               | 4        | 1            | 1    | 1234                 |
		| 1    | 3               | 2        | 1            | 1    | 2345 6789            |
		| 1    | 4               | 2        | 1            | 2    | 5678 1234  9876 5432 |
	And a register configuration with the following values
		| Unit | StartingAddress | DataType | FunctionCode | Size |
		| 1    | 1               | 4        | 1            | 1    |
		| 1    | 2               | 4        | 1            | 1    |
		| 1    | 3               | 2        | 1            | 1    |
		| 1    | 4               | 2        | 1            | 2    |

Scenario: Connecting to Modbus from ModbusBridge
	Given a connector configuration with the following values
		| Ip          | Port, | Endianness | Protocol | UseASCII | Interval | ReadTimeout |
		| "127.0.0.1" | 502   | 1          | 1        | true     | -1       | 1000        |
	When establishing connection to modbus
	Then the following ModbusRegisterReceived events should be emitted
		| Unit | StartingAddress | FunctionCode | Size | Content              |
		| 1    | 1               | 1            | 1    | ABCD                 |
		| 1    | 2               | 1            | 1    | 1234                 |
		| 1    | 3               | 1            | 1    | 2345 6789            |
		| 1    | 4               | 1            | 2    | 5678 1234  9876 5432 |
