using Campany.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly UserManager<Employee> userManager;

        public DataController(UserManager<Employee> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            //var user = User.Identity.Name;
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.GetUserAsync(User);

            return Ok(new { UserName = user, Email = user.Email });
        }

        [HttpGet]
        [Authorize(Policy = "admin")]
        [Route("ForManagers")]
        public ActionResult GetInfoForManager()
        {
            return Ok(new string[] {
            "Ahmed",
            "Muhammed",
            "Mona",
            "This Data From Managers Only"
        });
        }

        [HttpGet]
        [Authorize(Policy = "user")]
        [Route("ForUsers")]
        public ActionResult GetInfoForUser()
        {
            return Ok(new string[] {
            "Ahmed",
            "Muhammed",
            "Mona",
            "This Data From users"
        });
        }


    }
}
