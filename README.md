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

## Publishing a message to a queue
1. Start the web applicaiton
1. Do a ``POST`` to the URL ``~/api/messageQueue``
1. Set the request header ``Content-Type`` to ``application/json``
1. Set the ``body`` to:
```javascrit
{
	"data": "my message data",
	"messageType": 1
},
```
Use ``"messageType": 2`` to publish a message for all queues using the exchange+bind approuch.