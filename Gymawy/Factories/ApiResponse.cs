namespace Gymawy.Factories;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public ApiResponse(int statusCode, string message = null!)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }


    private string GetDefaultMessage(int statusCode)
       => statusCode switch
       {
           StatusCodes.Status404NotFound => "Not Found",
           StatusCodes.Status400BadRequest => "Bad Request",
           StatusCodes.Status500InternalServerError => "Internal Server Error",
           StatusCodes.Status401Unauthorized => "Unauthorized Error ",

           _ => null!
       };
}
