namespace e_commerce_app.Errors;

public class ApiResponse
{
    //Class
    //This class will be used to return a response to the client
    //that is flattened and has a status code and a message
    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        //The ??
        //operator is called the null-coalescing operator
        //It returns the left-hand operand if the operand is not null; otherwise it returns the right hand operand
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    
    public string Message { get; set; }
    
}