using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class StudentRepos : GenericRepository<Student>, IStudentRepos
    {
        public StudentRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
