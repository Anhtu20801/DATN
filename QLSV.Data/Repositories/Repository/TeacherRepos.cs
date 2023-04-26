﻿using QLSV.Data.Infrastructure;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.Repository
{
    public class TeacherRepos : GenericRepository<Teacher>, ITeacherRepos
    {
        public TeacherRepos(StudentDBContext _context) : base(_context)
        {
        }
    }
}
