using Education.Domains.School.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.BusinessServices.Contracts
{
    public interface ISchoolBusinessService
    {
        Task<Boolean> Resgister(RegistrationRequest registrationModel);
    }
}
