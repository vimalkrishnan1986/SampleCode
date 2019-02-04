using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
namespace EdcuationSystem.Core.Business.Interfaces
{
    public interface IMessageBusinessService
    {
        Task Send(object payLoad, string queueName);
    }
}
