using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Service;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // attributed routing and method to access service
        [HttpPost, Route("~/api/Auth/register")]
        public async Task<IActionResult> Createregister([FromHeader] string Signature, Register registers)
        {


            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }


            var data = await _unitOfWork.RegisterService.register(registers);

            return Ok(data);



        }
        [HttpPost, Route("~/api/Auth/login")]
        public async Task<IActionResult> Createlogin([FromHeader] string Signature, [FromBody] Login req)
        {
            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }

            var data = await _unitOfWork.LoginService.Logins(req);
            return Ok(data);
        }



    }
}
