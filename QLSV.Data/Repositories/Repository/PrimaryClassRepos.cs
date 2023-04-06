using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class PrimaryClassRepos : GenericRepository<PrimaryClassRepos>, IPrimaryClassRepos
    {
        public PrimaryClassRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
