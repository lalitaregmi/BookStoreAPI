using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Service;
using System.Threading.Channels;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("[Controller]")]

    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // attributed routing and method to access service
        [HttpPost, Route("~/api/authors")]
        public async Task<IActionResult> Createauthors([FromHeader] string Signature, Authors authors)
        {


            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }

            if (authors.AuthCode != "r1d3r")
            {
                resp.StatusCode = 355;
                resp.Message = "Session expired.";
                return Ok(resp);
            }

            var data = await _unitOfWork.AuthorsService.Authors(authors);

            return Ok(data);



        }

    }
}
