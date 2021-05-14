# Connectors.Modbus

[![.NET 5.0](https://github.com/RaaLabs/Modbus/actions/workflows/dotnet.yml/badge.svg)](https://github.com/RaaLabs/Modbus/actions/workflows/dotnet.yml)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Modbus&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=RaaLabs_Modbus)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Modbus&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_Modbus)

## What does it do?

The Modbus connector polls a Modbus register for data with a given interval.

The connector produces events to EdgeHub with the output name "output", and should be routed to [IdentityMapper](https://github.com/RaaLabs/IdentityMapper).

## Configuration

The module is configured using the following configuration files:

### connector.json

This file configures the connection to the Modbus server itself. It contains IP address and port, polling interval, read timeout,
[endianness](https://en.wikipedia.org/wiki/Endianness), protocol type (TCP/RTU), and ASCII mode.

A typical `connector.json` looks like this:

```json
{
    "ip": "host.docker.internal",
    "port": "502",
    "useASCII": false,
    "endianness": 4,
    "protocol": 2,
    "interval": 2000,
    "readTimeout": 1000
}
```

#### Endianness

These are the valid values for endianness:

| Value | Meaning              |
| ----- | -------------------- |
| 1     | High Byte, High Word |
| 2     | High Byte, Low Word  |
| 3     | Low Byte, High Word  |
| 4     | Low Byte, Low Word   |

#### Protocol

These are the valid values for protocol type:

| Value | Meaning       |
| ----- | ------------- |
| 1     | TCP           |
| 2     | RTU over TCP  |


### registers.json

This file configures which registers to fetch from the Modbus server. It consists of an array of register configurations, each of which
contain information about Modbus unit id, address, data type, function code and number of data points.

A typical `registers.json` looks like this:

```json
[
    {
        "unit": 1,
        "startingAddress": 100,
        "dataType": 3,
        "functionCode": 2,
        "size": 4
    },
    {
        "unit": 1,
        "startingAddress": 200,
        "dataType": 3,
        "functionCode": 2,
        "size": 4
    }
]
```

#### dataType

These are the valid values for data type:
| Value | Meaning |
| ----- | ------- |
| 1     | int32   |
| 2     | uint32  |
| 3     | float   |
| 4     | int16   |


#### functionCode

These are the valid values for function code:
| Value | Meaning          |
| ----- | ---------------- |
| 1     | Coil             |
| 2     | Holding Register |
| 3     | Input            |
| 4     | Input Register   |


## IoT Edge Deployment

### $edgeAgent

In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

The module has persistent state and it is assuming that this is in the `data` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the
volume binding.

```json
"Binds": [
    "<mount-path>:/app/data"
]
```

```json
{
    "modulesContent": {
        "$edgeAgent": {
            "properties.desired.modules.Modbus": {
                "settings": {
                    "image": "<repo-name>/connectors-modbus:<tag>",
                    "createOptions": "{\"HostConfig\":{\"Binds\":[\"<mount-path>:/app/data\"]}}"
                },
                "type": "docker",
                "version": "1.0",
                "status": "running",
                "restartPolicy": "always"
            }
        }
    }
}
```

For production setup, the bind mount can be replaced by a docker volume.

### $edgeHub

The routes in edgeHub can be specified like the example below.

```json
{
    "$edgeHub": {
        "properties.desired.routes.ModbusToIdentityMapper": "FROM /messages/modules/Modbus/outputs/output INTO BrokeredEndpoint(\"/modules/IdentityMapper/inputs/events\")",
    }
}
```
