using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class AttendanceRepos : GenericRepository<Attendance>, IAttendanceRepos
    {
        public AttendanceRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
