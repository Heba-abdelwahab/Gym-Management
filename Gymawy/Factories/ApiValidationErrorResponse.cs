using System.Text.Json;

namespace Gymawy.Factories;

public class ApiValidationErrorResponse : ApiResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ApiValidationErrorResponse()
        : base(StatusCodes.Status400BadRequest)
    {
        Errors = new List<string>();
    }

    public override string ToString()
    {

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(this, options);

    }


}


