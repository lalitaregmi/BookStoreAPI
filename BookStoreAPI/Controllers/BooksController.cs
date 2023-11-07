using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Service;

namespace BookStoreAPI.Controllers
{

    [ApiController]
    [EnableCors("CorsPolicy")]
    [Route("[Controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // attributed routing and method to access service
        [HttpPost, Route("~/api/books")]
        public async Task<IActionResult> Createauthors([FromHeader] string Signature, Books books)
        {


            var resp = new CommonResponse();
            if (Signature != "p0m76")
            {
                resp.StatusCode = 356;
                resp.Message = "Signature expired.";
                return Ok(resp);
            }

            if (books.AuthCode != "r1d3r")
            {
                resp.StatusCode = 355;
                resp.Message = "Session expired.";
                return Ok(resp);
            }

            var data = await _unitOfWork.BooksService.Books(books);

            return Ok(data);



        }
    }
}
