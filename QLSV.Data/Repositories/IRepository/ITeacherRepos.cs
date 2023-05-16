using QLSV.Data.Infrastructure;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.IRepository
{
    public interface ITeacherRepos : IGenericRepository<Teacher>
    {
        Teacher getByTeacherCode(string teacherCode);
    }
}
