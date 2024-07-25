using Azure.Core;
using DataLayer;
using DataLayer.Entities;
using FamsAPI.IServices;
using FamsAPI.Services;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FamsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IUser _userServices;
        private readonly IRefreshHandler _refreshHandler;

        //private readonly IRefreshHandler refresh;

        public AuthorizeController(IUser userServices, IRefreshHandler refreshHandler)
        {
            _userServices = userServices;
            _refreshHandler = refreshHandler;
        }
        #region GenerateToken
        /// <summary>
        /// Which will generating token accessible for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [NonAction]
        public TokenViewModel GenerateToken(User user, String? RT)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.Name),
                new Claim("Email", user.Email),
                new Claim("Role", user.PermissionId.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            if (RT != null)
            {
                return new TokenViewModel()
                {
                    AccessTokenToken = accessToken,
                    RefreshToken = RT,
                    ExpiredAt = _refreshHandler.GetRefreshTokenByUserID(user.UserId.ToString()).ExpireAt
                };
            }
            return new TokenViewModel()
            {
                AccessTokenToken = accessToken,
                RefreshToken = GenerateRefreshToken(user),
                ExpiredAt = _refreshHandler.GetRefreshTokenByUserID(user.UserId.ToString()).ExpireAt
            };
        }
        #endregion

        #region GenerateRefreshToken
        // Hàm tạo refresh token
        [NonAction]
        public string GenerateRefreshToken(User user)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken = Convert.ToBase64String(randomnumber);
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = user.UserId,
                    TokenId = new Random().Next().ToString(),
                    RefreshTokenString = refreshtoken.ToString(),
                    ExpireAt = DateTime.Now.AddDays(7),
                    Statuses = ReStatuses.Enable
                };

                _refreshHandler.GenerateRefreshToken(refreshTokenEntity);
                return refreshtoken;
            }
        }
        #endregion

        #region RefreshAccessToken
        [HttpPost("RefreshAccessToken")]
        public async Task<ActionResult> RefreshAccessToken(TokenViewModel token)
        {
            try
            {
                var jwtTokenHander = new JwtSecurityTokenHandler();
                var TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                //ResetRefreshToken in DB if token is disable or expired will Remove RT
                _refreshHandler.ResetRefreshToken();
                //check validate of Parameter
                var tokenVerification = jwtTokenHander.ValidateToken(token.AccessTokenToken, TokenValidationParameters, out var validatedToken);
                if (tokenVerification == null)
                {
                    return Ok(new APIResponse
                    {
                        Success = false,
                        Message = "Invalid Param"
                    });
                }
                //check AccessToken expire?
                var epochTime = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                DateTimeOffset dateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(epochTime);
                DateTime dateTimeUtcConverted = dateTimeUtc.UtcDateTime;
                if (dateTimeUtcConverted > DateTime.UtcNow)
                {
                    return Ok(new APIResponse
                    {
                        Success = false,
                        Message = "AccessToken had not expired",
                        data = "Expire time: " + dateTimeUtcConverted.ToString()
                    });
                }
                //check RefreshToken exist in DB
                var storedToken = _refreshHandler.GetRefreshToken(token.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new APIResponse
                    {
                        Success = false,
                        Message = "RefreshToken had not existed"
                    });
                }
                //check RefreshToken is revoked?
                if (storedToken.Statuses == ReStatuses.Disable)
                {
                    return Ok(new APIResponse
                    {
                        Success = false,
                        Message = "RefreshToken had been revoked"
                    });
                }
                var User = _userServices.GetUserById(storedToken.UserId);
                var newAT = GenerateToken(User, token.RefreshToken);

                return Ok(new APIResponse
                {
                    Success = true,
                    Message = "Refresh AT success fully",
                    data = newAT
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "Something go wrong"
                });
            }
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = _userServices.GetUserByEmail(email);
            if (user != null && user.Status == 0)
            {
                // Hash the input password with MD5
                var hashedInputPasswordString = UserServices.HashAndTruncatePassword(password);

                // Compare the hashed input password with the stored hashed password
                if (hashedInputPasswordString == user.Password)
                {
                    _refreshHandler.ResetRefreshToken();
                    var token = GenerateToken(user, null);
                    return Ok(token);
                }
            }
            return BadRequest(new APIResponse
            {
                Success = false,
                Message = "Status Code:401 Unauthorized | Invalid email or password",
                data = null
            });
        }
        #endregion

        #region Logout
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"];
                token = token.Split(' ')[1];
                var tokenHandler = new JwtSecurityTokenHandler();
                var TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = false
                };
                SecurityToken validatedToken;
                var claimsPrincipal = tokenHandler.ValidateToken(token, TokenValidationParameters, out validatedToken);
                var userIdClaim = claimsPrincipal.FindFirst("UserId");
                var _refreshToken = _refreshHandler.GetRefreshTokenByUserID(userIdClaim.Value);
                _refreshHandler.UpdateRefreshToken(_refreshToken);
                _refreshHandler.ResetRefreshToken();
                if (HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    HttpContext.Request.Headers.Remove("Authorization");
                }
                return Ok(new APIResponse
                {
                    Success = true,
                    Message = "Logout succesfully!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse
                {
                    Success = false,
                    Message = "Something go wrong" + ex.Message
                });
            }
        }
        #endregion
    }
}
