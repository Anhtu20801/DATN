using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class TeacherRepos : GenericRepository<TeacherRepos>, ITeacherRepos
    {
        public TeacherRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
