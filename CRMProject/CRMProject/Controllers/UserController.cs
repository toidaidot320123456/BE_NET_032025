using AutoMapper;
using DataAcccess.DTO;
using DataAcccess.IServices;
using DataAcccess.RequestData;
using DataAcccess.ResponseData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRMProject.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestData loginRequestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Login(loginRequestData);
            Response<LoginDTO> response = new Response<LoginDTO>(result);
            if (result != null)
            {
                // Bước 2 : tạo token   
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.UserName),
                    new Claim(ClaimTypes.PrimarySid, result.Id.ToString()),
                    new Claim(ClaimTypes.IsPersistent, result.IsAdmin.ToString())
                };

                var tokenNew = CreateToken(authClaims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenNew);

                var refreshToken = GenerateRefreshToken();
                var expiredRefreshToken = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JWT:RefreshTokenValidityInDays"]));
                await _userService.UpdateRefreshToken(new UserDTO()
                {
                    UserName = result.UserName,
                    ExpriedTime = expiredRefreshToken,
                    RefreshToken = refreshToken,
                });

                response.Data.Token = token;
                response.Data.RefreshToken = refreshToken;
                response.Success = true;
                response.Message = MessageResponse.SuccessAction;
                response.Status = StatusResponse.Success;
            }
            else
            {
                response.Success = false;
                response.Message = MessageResponse.FailureAction;
                response.Status = StatusResponse.Failure;
            }
            return Ok(response);
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        [HttpPost]
        [Route("GetRefreshToken")]
        public async Task<ActionResult> GetRefreshToken([FromBody] TokenRequestData tokenRequest)
        {
            Response<LoginDTO> response = new Response<LoginDTO>();
            var claimsPrincipal = GetPrincipalFromExpiredToken(tokenRequest.Token);
            if (claimsPrincipal == null)
            {
                response.Data = null;
                response.Success = false;
                response.Message = MessageResponse.FailureAction;
                response.Status = StatusResponse.Failure;
            }
            else
            {
                var userName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var userDTO = await _userService.GetByUserName(userName);
                if (userDTO == null || userDTO.RefreshToken != tokenRequest.RefreshToken || userDTO.ExpriedTime < DateTime.Now)
                {
                    return BadRequest(MessageResponse.InvalidToken);
                }

                var tokenNew = CreateToken(claimsPrincipal.Claims.ToList());
                var token = new JwtSecurityTokenHandler().WriteToken(tokenNew);

                var refreshToken = GenerateRefreshToken();
                var expiredRefreshToken = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JWT:RefreshTokenValidityInDays"]));
                await _userService.UpdateRefreshToken(new UserDTO()
                {
                    UserName = userName,
                    ExpriedTime = expiredRefreshToken,
                    RefreshToken = refreshToken,
                });
                response.Data = new LoginDTO() { Id = userDTO.Id, IsAdmin = userDTO.IsAdmin, UserName = userDTO.UserName };
                response.Data.Token = token;
                response.Data.RefreshToken = refreshToken;
                response.Success = true;
                response.Message = MessageResponse.SuccessAction;
                response.Status = StatusResponse.Success;


            }
            return Ok(response);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
