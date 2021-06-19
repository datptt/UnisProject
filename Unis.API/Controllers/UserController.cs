using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unis.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Unis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly UserBL _userBL;

        public UserController(UserBL userBL)
        {
            this._userBL = userBL;
        }

        
        // POST api/values
        [HttpPost]
        public async Task<ServiceResult> Login([FromBody] Dictionary<string,object> param)
        {
           return await this._userBL.Login(param.GetValueOrDefault("username")?.ToString(), param.GetValueOrDefault("password")?.ToString());
        }
    }
}
