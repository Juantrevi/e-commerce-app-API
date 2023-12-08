namespace e_commerce_app.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
    
    /*
    Class()
      This is the class that will be returned to the client when there is a validation error
      It will contain the status code and the errors flattened out
      Need to configure Program.cs to use this class as a service
    */
    public ApiValidationErrorResponse() : base(400)
    {
    }
    
    public IEnumerable<string> Errors { get; set; }
}