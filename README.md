# DotNetCoreRabbitMq
An example app of using **RabbitMq** on **DotNet Core**

## Pre requisits
* [.Net Core SDK](https://www.microsoft.com/net/core#windows)
* Instance of **RabbitMq** 

## Setup
The **RabbitMq** configuration should be defined in the ``appsettings-rabbitmq.json`` file.
After create the file, the configuration should be like this:
```javascript
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
```javascript
{
	"data": "my message data",
	"messageType": 1
}
```
Use ``"messageType": 2`` to publish a message for all queues using the exchange+bind approach.

## Consuming messages from the queue
You can use the ``QueueConsumerFactory`` class to create a manager for your consumers of a queue.
Is needed a ``ConsumerProperties`` to create a ``QueueConsumerManager``. 
A ``ConsumerProperties`` contains all information necessary to create a consumer, like the queue name, quantity of consumer workes...
The ``QueueConsumerManager`` give us the method ``Start()`` and ``Stop()`` to manager the queue consumers workes.
A usage sample is bellow:
```csharp
var consumerFactory = new QueueConsumerFactory(container);

var consumerProperties = ConsumerProperties.ForMultipleConsumers("MyQueue1", consumersQuantity: 2);
var consumerManager = consumerFactory.CreateConsumerManager<Queue1Service, Message>(consumerProperties);

consumerManager.Start();
// do something...
consumerManager.Stop();
```    
