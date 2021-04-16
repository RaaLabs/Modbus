Feature: EndiannessExtensions

Scenario: Converting from shorts to bytes
	Given the following shorts
		| DataType | Endianness       | Content                               |
		| Uint32   | HighByteHighWord | AABB CCDD  1122 3344  9988 7766       |
		| Uint32   | HighByteLowWord  | AABB CCDD  1122 3344  9988 7766       |
		| Uint32   | LowByteHighWord  | AABB CCDD  1122 3344  9988 7766       |
		| Uint32   | LowByteLowWord   | AABB CCDD  1122 3344  9988 7766       |
		| Int16    | HighByteHighWord | AABB CCDD  1122 3344  9988 7766       |
		| Int16    | HighByteLowWord  | AABB CCDD  1122 3344  9988 7766       |
		| Int16    | LowByteHighWord  | AABB CCDD  1122 3344  9988 7766       |
		| Int16    | LowByteLowWord   | AABB CCDD  1122 3344  9988 7766       |
		| Uint32   | HighByteHighWord | AABB CCDD  1122 3344  9988 7766  5544 |
		| Uint32   | HighByteLowWord  | AABB CCDD  1122 3344  9988 7766  5544 |
		| Uint32   | LowByteHighWord  | AABB CCDD  1122 3344  9988 7766  5544 |
		| Uint32   | LowByteLowWord   | AABB CCDD  1122 3344  9988 7766  5544 |
	When converting the shorts to bytes
	Then the shorts will be converted to the following bytes
		| Content                         |
		| DDCC BBAA  4433 2211  6677 8899 |
		| BBAA DDCC  2211 4433  8899 6677 |
		| CCDD AABB  3344 1122  7766 9988 |
		| AABB CCDD  1122 3344  9988 7766 |
		| BBAA DDCC  2211 4433  8899 6677 |
		| BBAA DDCC  2211 4433  8899 6677 |
		| AABB CCDD  1122 3344  9988 7766 |
		| AABB CCDD  1122 3344  9988 7766 |
	And the following input rows should throw an exception
		| RowNumber |
		| 9         |
		| 10        |
		| 11        |
		| 12        |