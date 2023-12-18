using Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors) 
    {
        if(errors.Count == 0)
        {
            return Problem();
        }

        if (errors.All(x => x.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors; 

        var firstError = errors[0];

        return Problem(firstError);
    }

    protected IActionResult ValidationProblem(List<Error> validationErrors)
    {
        var modelStateDict = new ModelStateDictionary();

        validationErrors.ForEach(x =>
        {
            modelStateDict.AddModelError(x.Code, x.Description);
        });

        return ValidationProblem(modelStateDict);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}
