using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public sealed class MessageBusClient : IMessageBusClient
    {
        private readonly ServiceBusSetting _serviceBusSetting;
        public MessageBusClient(ServiceBusSetting serviceBusSetting)
        {
            _serviceBusSetting = serviceBusSetting ?? throw new ArgumentNullException(nameof(serviceBusSetting));
        }

        public async Task Send(byte[] message, string queueName, string correlationId, Dictionary<string, string> properties)
        {
            try
            {

                var client = new QueueClient(_serviceBusSetting.ConnectionString,
                    queueName, ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);

                var brokeredMessage = new Message(message)
                {
                    CorrelationId = correlationId,
                };
                if (properties != null && properties.Count > 0)
                {
                    foreach (var prop in properties)
                    {
                        brokeredMessage.UserProperties.Add(prop.Key, prop.Value);
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
