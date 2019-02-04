using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public interface IQueueListener<T>
    {
        Task Recieve(BrokeredMessage arg);
        Task Subscribe(IProcessor<T> processor);
        Task Start();
        Task Stop();

    }
}
