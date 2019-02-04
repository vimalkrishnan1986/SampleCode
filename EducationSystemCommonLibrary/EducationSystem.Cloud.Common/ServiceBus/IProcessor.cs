using System.Threading.Tasks;

namespace EducationSystem.Cloud.Common.ServiceBus
{
    public interface IProcessor<T>
    {
        Task Process(T stream);
        string Name { get; }
    }
}
