Feature: ModbusRegisterReceivedHandler

Scenario: Handling incoming events
	Given a handler of type ModbusRegisterReceivedHandler
	When the following events of type ModbusRegisterReceived is produced
		| Unit | StartingAddress | DataType | FunctionCode | Size | Content              |
		| 1    | 1               | 4        | 1            | 1    | ABCD                 |
		| 1    | 2               | 4        | 1            | 1    | 1234                 |
		| 1    | 3               | 2        | 1            | 1    | C907 CC00            |
		| 1    | 5               | 2        | 1            | 2    | 5678 1234  9876 5432 |
		| 1    | 9               | 3        | 1            | 2    | DB0F 4940  54F8 2D40 |
		| 1    | 13              | 1        | 1            | 1    | C907 CC00            |
	Then the following events of type ModbusDatapointOutput is produced
		| Tag  | Source | Value     |
		| 1:1  | Modbus | -12885    |
		| 1:2  | Modbus | 13330     |
		| 1:3  | Modbus | 13371337  |
		| 1:5  | Modbus | 873625686 |
		| 1:7  | Modbus | 844396184 |
		| 1:9  | Modbus | 3.1415926 |
		| 1:11 | Modbus | 2.7182818 |
		| 1:13 | Modbus | 13371337  |