using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class CourseRepos : GenericRepository<Course>, ICourseRepos
    {
        public CourseRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
