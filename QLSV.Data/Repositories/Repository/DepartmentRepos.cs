using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class DepartmentRepos : GenericRepository<Department>, IDepartmentRepos
    {
        public DepartmentRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
