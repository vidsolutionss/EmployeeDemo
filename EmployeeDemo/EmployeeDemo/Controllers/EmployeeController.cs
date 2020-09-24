using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeeDemo.Models;
using EmployeeDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IConfiguration _configuration;
        public EmployeeController(IEmployeeService employeeService, IConfiguration config)
        {
            _employeeService = employeeService;
            _configuration = config;
        }

        [HttpPost("EmployeeInsert")]
        [Authorize]
        public async Task<IActionResult> EmployeeInsert([FromBody] Employees employees)
        {
            var result = await _employeeService.InsertEmployee(employees);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("EmployeeUpdate")]
        [Authorize]
        public async Task<IActionResult> EmployeeUpdate([FromBody] Employees employees)
        {
            var result = await _employeeService.UpdateEmployee(employees);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("EmployeeDelete/{id}")]
        [Authorize]
        public async Task<IActionResult> EmployeeDelete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("EmployeeGet/{id}")]
        public async Task<IActionResult> EmployeeGet(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet("EmployeeGetAll")]        
        public async Task<IActionResult> EmployeeGetAll()
        {
            var result = await _employeeService.GetEmployees();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpPost("CreateAccessToken")]
        public async Task<IActionResult> CreateAccessToken(Users user)
        {
            var result = await _employeeService.GetUserInfo(user.UserName, user.Password);

            if(result != null)
            {
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),                   
                    new Claim("UserName", user.UserName)                    
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
