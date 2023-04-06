using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class StudentRepos : GenericRepository<StudentRepos>, IStudentRepos
    {
        public StudentRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
