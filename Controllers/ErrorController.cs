using e_commerce_app.Errors;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_app.Controllers;

[Route("errors/{code}")]
public class ErrorController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
    
}