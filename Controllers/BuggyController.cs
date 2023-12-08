using e_commerce_app.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Controllers;

/*
 *Uses (Testing purposes):
 * To set up some errors to see how our error handling works
*/
public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;
    
    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        //A product that does not exists
        var thing = _context.Products.Find(42);
        if (thing == null)
        {
            return NotFound(new ApiResponse(404));
        }
        
        return Ok();
    }
    
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);
        
        //This will throw an exception because it cannot execute a .toString() on a null object
        var thingToReturn = thing.ToString();
        
        return Ok();
    }
    
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }
    
    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest401(int id)
    {
        return Ok();
    }
}