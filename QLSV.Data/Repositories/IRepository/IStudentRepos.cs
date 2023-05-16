using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.Repository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.IRepository
{
    public interface IStudentRepos : IGenericRepository<Student>
    {
        Student getByStudentCode(string studentCode);
    }
}
