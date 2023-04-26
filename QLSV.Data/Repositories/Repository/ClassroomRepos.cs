using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class ClassroomRepos : GenericRepository<Classroom>, IClassroomRepos
    {
        public ClassroomRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
