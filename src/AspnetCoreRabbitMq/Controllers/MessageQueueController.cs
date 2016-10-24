using DotNetCoreRabbitMq.Infrastructure.MessageQueue;
using DotNetCoreRabbitMq.Models;
using DotNetCoreRabbitMq.Queues;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreRabbitMq.Controllers
{
    [Route("api/[controller]")]
    public class MessageQueueController : Controller
    {
        private readonly IQueueClient _queueClient;

        public MessageQueueController(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            var messageIsValid = message?.Data != null &&
                                 message?.MessageType != MessageType.Unknown;
            if (!messageIsValid)
            {
                return BadRequest();
            }


            message.Source = nameof(MessageQueueController);

            var routingKey = message.MessageType == MessageType.Specific
                ? QueueConstants.NewMessageSpecificRoute
                : QueueConstants.NewMessageGenericRoute;

            _queueClient.Publish(QueueConstants.ExchangeName, routingKey, message);

            return Ok();
        }
    }
}
