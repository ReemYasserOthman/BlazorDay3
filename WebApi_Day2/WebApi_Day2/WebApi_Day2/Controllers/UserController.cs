using Campany.BL.Dots;
using Campany.BL.Managers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IAccManager _accManager;
        public UserController(IAccManager accManager)
        {
            _accManager = accManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {

            var result = await _accManager.Login(loginDto);
            if (result.Result != TokenResult.success) { return BadRequest(result); }

            return result;
        }


        [HttpPost]
        [Route("AdminRegister")]
        public async Task<ActionResult> AdminRegister(RegisterDto registerDto)
        {
            var result = await _accManager.Register(registerDto, "admin");
            if (!result.IsSuccess) { return BadRequest(result); }

            return NoContent();
        }
        [HttpPost]
        [Route("UserRegister")]
        public async Task<ActionResult> UserRegister(RegisterDto registerDto)
        {
            var result = await _accManager.Register(registerDto, "user");
            if (!result.IsSuccess) { return BadRequest(result); }
            return NoContent();

        }

    }
}
