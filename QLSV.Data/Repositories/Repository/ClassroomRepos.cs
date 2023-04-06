using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class ClassroomRepos : GenericRepository<ClassroomRepos>, IClassroomRepos
    {
        public ClassroomRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
