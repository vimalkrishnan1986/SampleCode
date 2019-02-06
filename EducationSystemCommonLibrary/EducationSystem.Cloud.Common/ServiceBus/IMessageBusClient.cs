using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public interface IMessageBusClient
    {
        Task Send(byte[] message, string queueName, string correlationId, Dictionary<string, string> properties);
    }
}
