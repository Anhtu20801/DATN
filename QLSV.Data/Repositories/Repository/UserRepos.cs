using Microsoft.AspNetCore.Identity;
using QLSV.Common;
using QLSV.Data.Repositories.IRepository;
using QLSV.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Data.Repositories.Repository
{
    public class UserRepos : IUserRepos
    {
        private readonly StudentDBContext _studentDBContext;
        private readonly UserManager<User> _userManager;
        public UserRepos(StudentDBContext studentDBContext, UserManager<User> userManager) 
        {
            _studentDBContext = studentDBContext;
            _userManager = userManager;   
        }
        public IEnumerable<User> GetAll()
        {
            return _userManager.Users;
        }
        
        public async Task<IdentityResult> Add(User user, string? role = null)
        {
            var result = await _userManager.CreateAsync(user,"Abc@123");
            if(result.Succeeded)
            {
                if(role != null)
                    await _userManager.AddToRoleAsync(user, role);
                else
                    await _userManager.AddToRoleAsync(user, RolesHelper.Role_Student);
            }
            return result;
        }

        public void Delete(string userName)
        {
            User? user = _studentDBContext.Users.FirstOrDefault(u=> u.UserName == userName);
            if(user != null)
            {
                _userManager.DeleteAsync(user).GetAwaiter().GetResult();
                
            }
        }


        public async Task<IdentityResult> Update(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
