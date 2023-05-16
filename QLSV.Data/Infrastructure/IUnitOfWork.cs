using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IDepartmentRepos DepartmentRepos { get; }
        IMajorRepos MajorRepos { get; }
        ITeacherRepos TeacherRepos { get; }
        IPrimaryClassRepos PrimaryClassRepos { get; }
        IStudentRepos StudentRepos { get; }
        ICourseRepos CourseRepos { get; }
        IClassroomRepos ClassroomRepos { get; }
        IResultRepos ResultRepos { get; }
        IAttendanceRepos AttendanceRepos { get; }
        IUserRepos UserRepos { get; }
        StudentDBContext StudentDBContext { get; }
        int SaveChange();

        Task<int> SaveChangeAsync();
    }
}
