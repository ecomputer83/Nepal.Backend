using Nepal.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Abstraction.Service.Business
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(RegisterModel model, IEnumerable<string> roleNames);
        Task<UserModel> GetUserById(string userId);
        Task<UserModel> GetUsersByiPManCode(string ipmancode);
        Task<IEnumerable<UserModel>> GetUsers(string userId);
        Task<bool> UpdateUser(UserModel user, IEnumerable<string> roleNames);
        Task ForgotPassword(EmailModel model);
        Task ResendVerificationEmail(EmailModel model);
        Task ResetPassword(ResetPasswordModel model);
        Task ConfirmEmail(ConfirmEmailModel model);
        Task<UserModel> FindByEmailAsync(string email);
        Task<UserModel> FindByIdAsync(string email);
        Task<UserModel> FindByUserNameAsync(string email);
        Task<IList<Claim>> GetClaimsAsync(UserModel user);
        Task<IList<string>> GetRolesAsync(UserModel user);
       Task<bool> CheckPasswordAsync(UserModel user, string Password);
        Task<IEnumerable<string>> GetRoles();
        Task AddRole(string roleName);
    }
}
