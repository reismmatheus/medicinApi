using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedicinApi.Helpers;
using MedicinApi.Models;
using MedicinApi.Repositories;
using MedicinApi.Repositories.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MedicinApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserController(
            IUserRepository userRepository,
            IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] Auth model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username ou senha incorreto." });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("d3e84769b4fce34f69a850e42b25282c");  //_appSettings.Secret
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Username = user.Username,
                Name = user.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Register model)
        {
            var user = new Repositories.Model.User
            {
                Name = model.Name,
                Username = model.Username
            };

            try
            {
                // create user
                _userRepository.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users = _userRepository.GetAll();
        //    var model = _mapper.Map<IList<Repositories.Model.User>>(users);
        //    return Ok(model);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var user = _userRepository.GetById(id);
        //    var model = _mapper.Map<Repositories.Model.User>(user);
        //    return Ok(model);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] UpdateModel model)
        //{
        //    // map model to entity and set id
        //    var user = _mapper.Map<User>(model);
        //    user.Id = id;

        //    try
        //    {
        //        // update user 
        //        _userRepository.Update(user, model.Password);
        //        return Ok();
        //    }
        //    catch (AppException ex)
        //    {
        //        // return error message if there was an exception
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    _userRepository.Delete(id);
        //    return Ok();
        //}
    }
}
