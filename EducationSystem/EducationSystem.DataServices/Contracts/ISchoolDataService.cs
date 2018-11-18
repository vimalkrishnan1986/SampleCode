using System.Threading.Tasks;
using Education.Domains.School.Entities;

namespace Education.DataServices.Contracts
{
    public interface ISchoolDataService
    {
        Task<bool> Register(RegistrationModel model);
    }
}
