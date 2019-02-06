using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public interface IQueueListener<T>
    {
        Task Recieve(Message arg, CancellationToken cancellationToken);
        Task Subscribe(IProcessor<T> processor);
        Task Start();
        Task Stop();

    }
}
