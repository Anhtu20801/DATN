using QLSV.Data.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IDepartmentRepos DepartmentRepos { get; }
        ITeacherRepos TeacherRepos { get; }
        IPrimaryClassRepos PrimaryClassRepos { get; }
        IStudentRepos StudentRepos { get; }
        ICourseRepos CourseRepos { get; }
        IClassroomRepos ClassroomRepos { get; }
        IResultRepos ResultRepos { get; }
        IAttendanceRepos AttendanceRepos { get; }

        StudentDBContext StudentDBContext { get; }
        int SaveChange();

        Task<int> SaveChangeAsync();
    }
}
