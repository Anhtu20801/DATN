using QLSV.Data.Repositories.IRepository;
using QLSV.Data.Repositories.Repository;

namespace QLSV.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDBContext _context;
        private IDepartmentRepos departmentRepos;
        private ITeacherRepos teacherRepos;
        private IPrimaryClassRepos primaryClassRepos;
        private IStudentRepos studentRepos;
        private ICourseRepos courseRepos;
        private IClassroomRepos classroomRepos;
        private IResultRepos resultRepos;
        private IAttendanceRepos attendanceRepos;

        public UnitOfWork(StudentDBContext context)
        {
            this._context = context;
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