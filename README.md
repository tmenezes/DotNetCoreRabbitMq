# DotNetCoreRabbitMq
An example app of using **RabbitMq** on **DotNet Core**

## Pre requisits
* [.Net Core SDK](https://www.microsoft.com/net/core#windows)
* Instance of **RabbitMq** 

## Setup
The **RabbitMq** configuration should be defined in the ``appsettings-rabbitmq.json`` file.
After create the file, the configuration should be like this:
```javascrit
{
    "QueueConnectionSettings": {
        "HostName": "localhost",
        "VirtualHost": "",
        "UserName": "guest",
        "Password": "guest"
    }
}
```