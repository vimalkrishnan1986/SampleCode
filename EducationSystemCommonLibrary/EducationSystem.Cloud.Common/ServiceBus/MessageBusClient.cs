using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.ServiceBus.Messaging;
using System.Collections.Generic;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public sealed class MessageBusClient : IMessageBusClient
    {
        private readonly MessagingFactory _factory;
        public MessageBusClient(ServiceBusSetting serviceBusSetting)
        {
            var _messageBus = serviceBusSetting ?? throw new ArgumentNullException(nameof(serviceBusSetting));
            _factory = MessagingFactory.CreateFromConnectionString(_messageBus.ConnectionString);
        }

        public async Task Send(Stream message, string queueName, string correlationId, Dictionary<string, string> properties)
        {
            try
            {

                var client = _factory.CreateQueueClient(queueName);
                var brokeredMessage = new BrokeredMessage(message)
                {
                    CorrelationId = correlationId,
                };
                if (properties != null && properties.Count > 0)
                {
                    foreach (var prop in properties)
                    {
                        brokeredMessage.Properties.Add(prop.Key, prop.Value);
                    }
                }

                await client.SendAsync(brokeredMessage);
            }
            catch
            {
                throw;
            }
        }
    }
}
