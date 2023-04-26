using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class ResultRepos : GenericRepository<Result>, IResultRepos
    {
        public ResultRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
