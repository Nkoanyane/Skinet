using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;
        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("NotFound")]
        public ActionResult getNotFoundRequest()
        {
            var thing =context.Products.Find(42);
            if (thing==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult getBadRequest(int id)
        {
            
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult getServerError()
        {
            var thing =context.Products.Find(42);
            var thingToReturn =thing.ToString();
            return Ok(new ApiResponse(500));
        }
        [HttpGet("badrequest")]
        public ActionResult getNotBadRequestt()
        {
           
            return BadRequest();
        }
    }
}