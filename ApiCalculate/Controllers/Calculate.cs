using ApiCalculate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculateApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Calculate : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public Calculate(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        public ActionResult Post(double num1, double num2, [FromHeader][Required] char opt) 
        {
            switch (opt)
            {
                case '+':
                    return Ok(num1 + num2);
                case '-':
                    return Ok(num1 - num2);
                case '/':
                    return Ok(num1 / num2);
                case '*':
                    return Ok(num1 * num2);
                default:
                    return BadRequest("Operator attribute not matching available oprations (+ - * /)");
            }
        }
    }
}
