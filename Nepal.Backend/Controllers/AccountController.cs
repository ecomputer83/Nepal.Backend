using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using Nepal.Abstraction.Model;
using Nepal.Abstraction.Service.Business;
using Nepal.Backend.Helpers;
using Nepal.Backend.Settings;

namespace Nepal.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly JwtSecurityTokenSettings _jwt;
        private readonly ILogger<AccountController> _logger;
        private readonly IKYCClientService _kYCClientService;
        private readonly IMapper _mapper;

        public AccountController(
            IConfiguration configuration,
            IUserService userService,
            IKYCClientService kYCClientService,
        IOptions<JwtSecurityTokenSettings> jwt,
            ILogger<AccountController> logger,
            IMapper mapper
            )
        {

            this._configuration = configuration;
            this._userService = userService;
            this._kYCClientService = kYCClientService;
            this._jwt = jwt.Value;
            this._logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// Confirms a user email address
        /// </summary>
        /// <param name="model">ConfirmEmailViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailModel model)
        {
            try
            {
                if (model.Email == null || model.Code == null)
                {
                    return BadRequest(new string[] { "Error retrieving information!" });
                }

                await _userService.ConfirmEmail(model);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;

                var users = await _userService.GetUsers(userId);

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("AllAdminUsers")]
        public async Task<IActionResult> AllAdminUsers()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;

                var users = await _userService.GetAdminUsers(userId);

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("AddCreditLimit/{id}/{limit}")]
        public async Task<IActionResult> AddCreditLimit(string id, long limit)
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;

                await _userService.AddCreditLimit(id, limit);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userId = HttpContext.User.Identity.Name;

                var user = await _userService.GetUserById(userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {

                var user = await _userService.GetRoles();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="model">RegisterViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                await _userService.CreateUserAsync(model, null);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("addUser")]
        public async Task<IActionResult> AddUser([FromBody]UserManualModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
                var user = _mapper.Map<RegisterModel>(model);
                user.Email = model.Email;
                user.Password = "password";
                user.ConfirmPassword = "password";
                await _userService.CreateUserAsync(user, null);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("addAdmn")]
        public async Task<IActionResult> AddAdmin([FromBody]AdminModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));
                
                var _model = _mapper.Map<RegisterModel>(model);
                _model.Email =
                _model.Password = "password";
                _model.ConfirmPassword = "password";
                await _userService.CreateUserAsync(_model, model.roleNames);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }



        /// <summary>
        /// Log into account
        /// </summary>
        /// <param name="model">LoginViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModel model)
        {
            try
            {
                var user = await _userService.FindByEmailAsync(model.Email).ConfigureAwait(false);
                if (user == null)
                    return BadRequest(new string[] { "Invalid credentials." });

                var tokenModel = new TokenModel()
                {
                    HasVerifiedEmail = false
                };



                // Used as user lock
                if (user.IsLockout)
                    return BadRequest(new string[] { "This account has been locked." });

                if (await _userService.CheckPasswordAsync(user, model.Password).ConfigureAwait(false))
                {
                    tokenModel.HasVerifiedEmail = true;

                    if (user.TwoFactorEnabled)
                    {
                        tokenModel.TFAEnabled = true;
                        return Ok(tokenModel);
                    }
                    else
                    {
                        JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user).ConfigureAwait(false);
                        tokenModel.TFAEnabled = false;
                        tokenModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                        return Ok(tokenModel);
                    }
                }

                return BadRequest(new string[] { "Invalid login attempt." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }



        /// <summary>
        /// Forgot email sends an email with a link containing reset token
        /// </summary>
        /// <param name="model">ForgotPasswordViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]EmailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                await _userService.ForgotPassword(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("addrole")]
        public async Task<IActionResult> AddRole([FromBody]RoleModel model)
        {
            try
            {
                await _userService.AddRole(model.Name);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        /// <summary>
        /// Reset account password with reset token


        /// <summary>
        /// Resend email verification email with token link
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("resendVerificationEmail")]
        public async Task<IActionResult> resendVerificationEmail([FromBody]EmailModel model)
        {
            try
            {
                await _userService.ResendVerificationEmail(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

                await _userService.ResetPassword(model);
                    
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("removeuser/{Id}")]
        public async Task<IActionResult> RemoveUser(string Id)
        {
            try
            {
                await _userService.RemoveUser(Id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return CreateApiException(ex);
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(UserModel user)
        {
            var userClaims = await _userService.GetClaimsAsync(user).ConfigureAwait(false);
            var roles = await _userService.GetRolesAsync(user).ConfigureAwait(false);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id),
                new Claim(ClaimTypes.Name, user.Id),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}