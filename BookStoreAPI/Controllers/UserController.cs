using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Service;
using System.Threading.Channels;

namespace happyhomes.Controllers
{

    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // attributed routing and method to access service
        [HttpPost, Route("~/api/user")]
        public async Task<IActionResult> Createuser([FromHeader] string? Signature, User users)
        {
            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }

            if (users.AuthCode != "r1d3r")
            {
                resp.StatusCode = 355;
                resp.Message = "Session expired.";
                return Ok(resp);
            }


            var data = await _unitOfWork.UserService.user(users);

            return Ok(data);



        }

    }
}
