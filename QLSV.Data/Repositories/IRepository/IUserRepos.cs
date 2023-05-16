using Microsoft.AspNetCore.Identity;
using QLSV.Model.Models;

namespace QLSV.Data.Repositories.IRepository
{
    public interface IUserRepos
    {
        IEnumerable<User> GetAll();
        Task<IdentityResult> Add(User user, string? role = null);
        Task<IdentityResult> Update(User user);
        void Delete(string userName);
    }
}
