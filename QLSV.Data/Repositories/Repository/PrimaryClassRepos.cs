using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class PrimaryClassRepos : GenericRepository<PrimaryClass>, IPrimaryClassRepos
    {
        public PrimaryClassRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
