namespace e_commerce_app.Errors;

public class ApiResponse
{
    /*
    Class()
      This class will be used to return a response to the client
      that is flattened and has a status code and a message
    */

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode; 
    /*
    The ??
    operator is called the null-coalescing operator
    It returns the left-hand operand if the operand is not null; otherwise it returns the right hand operand
    */
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }
    public int StatusCode { get; set; }
    
    public string Message { get; set; }
    
    
    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        
    /*
    Switch statement
    will return a message depending on the status code
    that we pass in
    */
        return statusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Errors are the path to the dark side. " +
                   "Errors lead to anger. " +
                   "Anger leads to hate. " +
                   "Hate leads to career change",
            _ => null
        };
    }
    
}