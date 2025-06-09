using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Gymawy.Factories;

public class ApiResponseFactory
{
    public static IActionResult CustomValidationErrorResponse(ActionContext context)
    {

        var errors = context.ModelState.Where(model => model.Value.Errors.Any())
                                               .SelectMany(model => model.Value.Errors)
                                               .Select(error => error.ErrorMessage);

        var response = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    }
}
