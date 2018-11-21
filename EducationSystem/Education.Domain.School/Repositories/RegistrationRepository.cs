using Microsoft.EntityFrameworkCore;
using Education.Domains.School.Entities;
using Education.Data.Common;

namespace Education.Domains.School.Repositories
{
    public sealed class RegistrationRepository : BaseRepository<RegistrationRequest>
    {
        public RegistrationRepository(SchoolDBContext dbContext) :
            base(dbContext)
        {

        }
    }
}
