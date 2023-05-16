using Microsoft.AspNetCore.Identity;
using QLSV.Data.Repositories.IRepository;
using QLSV.Data.Repositories.Repository;
using QLSV.Model.Models;

namespace QLSV.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDBContext _context;
        private readonly UserManager<User> _userManager;

        private IDepartmentRepos departmentRepos;
        private IMajorRepos majorRepos;
        private ITeacherRepos teacherRepos;
        private IPrimaryClassRepos primaryClassRepos;
        private IStudentRepos studentRepos;
        private ICourseRepos courseRepos;
        private IClassroomRepos classroomRepos;
        private IResultRepos resultRepos;
        private IAttendanceRepos attendanceRepos;
        private IUserRepos userRepos;
        public UnitOfWork(StudentDBContext context, UserManager<User> _userManager)
        {
            this._context = context;
            this._userManager = _userManager;
        }
        public IDepartmentRepos DepartmentRepos
        {
            get
            {
                if (this.departmentRepos == null)
                {
                    departmentRepos = new DepartmentRepos(this._context);
                }
                return departmentRepos;
            }
        }
        public IMajorRepos MajorRepos
        {
            get
            {
                if (this.majorRepos == null)
                {
                    majorRepos = new MajorRepos(this._context);
                }
                return majorRepos;
            }
        }

        public ITeacherRepos TeacherRepos
        {
            get
            {
                if (teacherRepos == null)
                {
                    teacherRepos = new TeacherRepos(this._context);
                }
                return teacherRepos;
            }
        }

        public IPrimaryClassRepos PrimaryClassRepos
        {
            get
            {
                if (primaryClassRepos == null)
                {
                    primaryClassRepos = new PrimaryClassRepos(this._context);
                }
                return primaryClassRepos;
            }
        }

        public IStudentRepos StudentRepos
        {
            get
            {
                if (studentRepos == null)
                {
                    studentRepos = new StudentRepos(this._context);
                }
                return studentRepos;
            }
        }

        public ICourseRepos CourseRepos
        {
            get
            {
                if (courseRepos == null)
                {
                    courseRepos = new CourseRepos(this._context);
                }
                return courseRepos;
            }
        }

        public IClassroomRepos ClassroomRepos
        {
            get
            {
                if (classroomRepos == null)
                {
                    classroomRepos = new ClassroomRepos(this._context);
                }
                return classroomRepos;
            }
        }

        public IResultRepos ResultRepos
        {
            get
            {
                if (resultRepos == null)
                {
                    resultRepos = new ResultRepos(this._context);
                }
                return resultRepos;
            }
        }

        public IAttendanceRepos AttendanceRepos
        {
            get
            {
                if (attendanceRepos == null)
                {
                    attendanceRepos = new AttendanceRepos(this._context);
                }
                return attendanceRepos;
            }
        }
        public IUserRepos UserRepos
        {
            get
            {
                if (userRepos == null)
                {
                    userRepos = new UserRepos(this._context, this._userManager);
                }
                return userRepos;
            }
        }

        public StudentDBContext StudentDBContext => this._context;

        public int SaveChange()
        {
            return this._context.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}