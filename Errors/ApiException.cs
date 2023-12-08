namespace e_commerce_app.Errors;

/*
 *Class()
    Made to return a response with more information than
    the message and status code, here we add where the error
    occurred
*/

/*
  Create Middleware to use this class
  and handle the exceptions (Exceptions/ExceptionMiddleware.cs
 */

public class ApiException : ApiResponse
{
    /*
     Add
     string details to the constructor
     */
    public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
    {
        Details = details;
    }
    
    public string Details { get; set; }
    
}