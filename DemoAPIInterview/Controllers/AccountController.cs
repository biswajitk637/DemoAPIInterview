using DemoAPIInterview.Contracts.Request;
using DemoAPIInterview.Contracts.Response;
using DemoAPIInterview.Models;
using DemoAPIInterview.Respository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DemoAPIInterview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUsers UserService;

        public AccountController(IUsers users, IConfiguration config)
        {
            this.UserService = users;
            _config = config;
        }

        private IConfiguration _config { get; }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult SignIn(SignInModel signInModel)
        {
            var user = UserService.SignIn(signInModel);
            var apiResponse = new ApiResponse();
            if (signInModel != null) {
                if (user == null)
                {
                    apiResponse.Ok = false;
                    apiResponse.Status = 404;
                    apiResponse.Message = "Invalid Login Credential !";
                    return Ok(apiResponse);
                }
                else
                {
                    string token = GenerateJSONWebToken();
                    apiResponse.Ok = true;
                    apiResponse.Status = 200;
                    apiResponse.Message = "Login Success !";
                    apiResponse.Data= user;
                    apiResponse.Token = token;
                    return Ok(apiResponse);
                }
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(SignUpModel signUpModel)
        {
            var user = UserService.SignUp(signUpModel);
            var apiResponse = new ApiResponse();
            if (signUpModel != null)
            {
                
                apiResponse.Ok = true;
                apiResponse.Status = 200;
                apiResponse.Message = "User Created Successfully !";
                apiResponse.Data = user;
                return Ok(apiResponse); 
            }
            else
            {
                return BadRequest();
            }

        }



        private string GenerateJSONWebToken()
        {
            var key = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(key) || key.Length < 32)
            {
                throw new InvalidOperationException("JWT Key must be at least 256 bits (32 bytes) long.");
            }

            // Ensure that _config is properly initialized and contains the necessary JWT settings
            if (_config == null || string.IsNullOrEmpty(_config["Jwt:Key"]) || string.IsNullOrEmpty(_config["Jwt:Issuer"]))
            {
                throw new InvalidOperationException("JWT configuration is not properly set up.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        //private string GenerateJSONWebToken()
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Issuer"],
        //      null,
        //      expires: DateTime.Now.AddMinutes(120),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


    }
}
