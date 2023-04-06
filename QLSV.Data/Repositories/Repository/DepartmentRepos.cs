using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class DepartmentRepos : GenericRepository<DepartmentRepos>, IDepartmentRepos
    {
        public DepartmentRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
