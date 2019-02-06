using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EdcuationSystem.Core.Business.Interfaces;
using EducationSystem.Cloud.Common.ServiceBus;

namespace EdcuationSystem.Core.Business
{
    public sealed class MessageBusinessService : IMessageBusinessService
    {
        private readonly IMessageBusClient _messageBusClient;

        public MessageBusinessService(IMessageBusClient messageBusClient)
        {
            _messageBusClient = messageBusClient ?? throw new ArgumentNullException(nameof(messageBusClient));
        }

        public async Task Send(object payLoad, string queueName)
        {
            byte[] utf8MessageBytes = Encoding.UTF8.GetBytes(payLoad.ToString());
            await _messageBusClient.Send(utf8MessageBytes, queueName, Guid.NewGuid().ToString(), null);
        }
    }
}
