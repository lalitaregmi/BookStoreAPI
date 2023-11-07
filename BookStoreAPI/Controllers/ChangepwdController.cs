using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Org.BouncyCastle.Ocsp;
using Service;

namespace happyhomes.Controllers
{

    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("[Controller]")]
    public class ChangepwdController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;


        public ChangepwdController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // attributed routing and method to access service
        [HttpPost, Route("~/api/change-password")]
        public async Task<IActionResult> Createlogin([FromHeader] string Signature, ChangePassword changes)
        {


            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }

            if (changes.AuthCode != "r1d3r")
            {
                resp.StatusCode = 355;
                resp.Message = "Session expired.";
                return Ok(resp);
            }

            var data = await _unitOfWork.ChngpwdService.changepassword(changes);

            return Ok(data);



        }

    }
}
