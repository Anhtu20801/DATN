using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QLSV.Common;
using QLSV.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Data
{
    public class DbInitialUser : IDbInitialUser
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StudentDBContext _context;
        
        public DbInitialUser(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            StudentDBContext context
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public void InitialUser()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                    _context.Database.Migrate();
            }
            catch
            {

            }
            
            _roleManager.CreateAsync(new IdentityRole(RolesHelper.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesHelper.Role_Teacher)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesHelper.Role_Student)).GetAwaiter().GetResult();
            //Add "Admin" user with admin role
            _userManager.CreateAsync(new User
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
            }, "Admin123*").GetAwaiter().GetResult();

            User? adminUser = _context.Users.FirstOrDefault(u => u.UserName == "Admin");
            if (adminUser != null) {
                _userManager.AddToRoleAsync(adminUser, RolesHelper.Role_Admin).GetAwaiter().GetResult();
            }
            //Add teacher user
            _userManager.CreateAsync(new User
            {
                UserName = "2009601375",
                Email = "phuonglan@gmail.com",
            }, "Teacher123*").GetAwaiter().GetResult();

            User? teacherUser = _context.Users.FirstOrDefault(u => u.UserName == "2009601375");
            if (teacherUser != null)
            {
                _userManager.AddToRoleAsync(teacherUser, RolesHelper.Role_Teacher).GetAwaiter().GetResult();
            }

            //Add student user
            _userManager.CreateAsync(new User
            {
                UserName = "2019601375",
                Email = "phuonglan@gmail.com",
            }, "Abc@123").GetAwaiter().GetResult();

            User? studentUser = _context.Users.FirstOrDefault(u => u.UserName == "2019601375");
            if (studentUser != null)
            {
                _userManager.AddToRoleAsync(studentUser, RolesHelper.Role_Student).GetAwaiter().GetResult();
            }
        }
    }
}
