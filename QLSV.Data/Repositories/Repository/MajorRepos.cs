using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Repository
{
    public class MajorRepos : GenericRepository<Major>, IMajorRepos
    {
        public MajorRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
