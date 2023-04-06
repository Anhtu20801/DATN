using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class AttendanceRepos : GenericRepository<AttendanceRepos>, IAttendanceRepos
    {
        public AttendanceRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
