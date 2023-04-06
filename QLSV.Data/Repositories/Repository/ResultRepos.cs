using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;

namespace QLSV.Data.Repositories.Repository
{
    public class ResultRepos : GenericRepository<ResultRepos>, IResultRepos
    {
        public ResultRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
