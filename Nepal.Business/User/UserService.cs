using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.EF.DB.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class UserService : IUserService
    {
        public readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task AddRole(string roleName)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
        }

        public async Task<bool> CheckPasswordAsync(UserModel user, string Password)
        {
            var _user = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.CheckPasswordAsync(_user, Password);
        }

        public async Task ConfirmEmail(ConfirmEmailModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
                throw new Exception("Could not find user!");

            await _userManager.ConfirmEmailAsync(user, model.Code).ConfigureAwait(false);
        }

        public async Task<bool> CreateUserAsync(RegisterModel model, IEnumerable<string> roleNames)
        {
            bool res = false;
            var user = _mapper.Map<User>(model);
            user.UserName = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                if (roleNames != null)
                {
                    var resultx = await _userManager.AddToRolesAsync(user, roleNames);
                    res = resultx.Succeeded;
                }
                else
                {
                    res = result.Succeeded;
                }
            }
            else
            {
                throw new Exception(result.Errors.Select(c => c.Description).Aggregate((a, b) => a + ", " + b));
            }
            return res;
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var _user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserModel>(_user);
        }

        public async Task<UserModel> FindByIdAsync(string id)
        {
            var _user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserModel>(_user);
        }

        public async Task<UserModel> FindByUserNameAsync(string name)
        {
            var _user = await _userManager.FindByNameAsync(name);
            return _mapper.Map<UserModel>(_user);
        }

        public async Task ForgotPassword(EmailModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait(false)))
                throw new Exception("Please verify your email address.");

            var code = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        }

        public async Task<IList<Claim>> GetClaimsAsync(UserModel user)
        {
            var _user = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.GetClaimsAsync(_user);
        }

        public Task<IEnumerable<string>> GetRoles()
        {
            IEnumerable<string> roles = _roleManager.Roles.Select(c=>c.Name).ToList();

            return Task.FromResult(roles);
        }

        public async Task<IList<string>> GetRolesAsync(UserModel user)
        {
            var _user = await _userManager.FindByIdAsync(user.Id);
            return await _userManager.GetRolesAsync(_user);
        }

        public async Task<UserModel> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserModel>(user);
        }

        public Task<IEnumerable<UserModel>> GetUsers(string userId)
        {
            var users = _userManager.Users;
            return Task.FromResult(_mapper.Map<IEnumerable<UserModel>>(users));
        }

        public Task<UserModel> GetUsersByiPManCode(string ipmancode)
        {
            var users = _userManager.Users.FirstOrDefault(c => c.IPMANCode == ipmancode && c.isIPMAN);
            return Task.FromResult(_mapper.Map<UserModel>(users));
        }

        public async Task ResendVerificationEmail(EmailModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
                throw new Exception("Could not find user!");

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                throw new Exception("Invalid credentials.");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password).ConfigureAwait(false);
        }

        public async Task<bool> UpdateUser(UserModel user, IEnumerable<string> roleNames)
        {
            var _user = await _userManager.FindByIdAsync(user.Id);
            if(_user == null)
                throw new Exception("Could not find user!");

            _user.BusinessName = user.BusinessName;
            _user.Address = user.Address;
            _user.ContactName = user.ContactName;
            _user.CreditBalance = user.CreditBalance;
            _user.CreditLimit = user.CreditLimit;
            _user.RCNumber = user.RCNumber;

            var result = await _userManager.UpdateAsync(_user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(c => c.Description).Aggregate((a, b) => a + ", " + b));
            }
            return result.Succeeded;
        }
    }
}
