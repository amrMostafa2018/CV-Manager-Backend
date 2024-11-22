using Ardalis.GuardClauses;
using Basic.Application.Common.Enums;
using Basic.Application.Common.Exceptions;
using Basic.Application.Common.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Basic_.net_Core_Solution.Infrastructure
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
        private readonly IProblemDetailsService _problemDetailsService;

        public CustomExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(BadHttpRequestException), HandleBadRequestException },
                { typeof(Exception),HandleGlobalException},
            };
            _problemDetailsService = problemDetailsService;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();
            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
                return true;
            }
            await _exceptionHandlers[typeof(Exception)].Invoke(httpContext, exception);
            return true;
        }

        private async Task HandleValidationException(HttpContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(
                new ResponseVM
                {
                    Error = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Type = ex.GetType().Name,
                        Title = "One Or More Validation Occured",
                        Detail = exception.Errors.Values.FirstOrDefault()?[0],
                        Extensions =
                        {
                       ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                        },
                        Instance = httpContext.Request.Method
                    },
                    OperationMessage = exception.Message,
                    OperationStatus = ResponseMessageStatusEnum.InValidData,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                }
                );
        }

        private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
        {
            var exception = (NotFoundException)ex;

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(
               new ResponseVM
               {
                   Error = new ProblemDetails
                   {
                       Status = StatusCodes.Status404NotFound,
                       Type = ex.GetType().Name,
                       Title = "Not Found Exception",
                       Detail = exception.Message,
                       Extensions =
                       {
                      ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                       },
                       Instance = httpContext.Request.Method
                   },
                   OperationMessage = exception.Message,
                   OperationStatus = ResponseMessageStatusEnum.NotFound,
                   StatusCode = System.Net.HttpStatusCode.NotFound
               }
               );

        }

        private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
        {
            var exception = (UnauthorizedAccessException)ex;

            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await httpContext.Response.WriteAsJsonAsync(
            new ResponseVM
            {
                Error = new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Type = ex.GetType().Name,
                    Title = "Un-Authorized Exception",
                    Detail = exception.Message,
                    Extensions =
                    {
                   ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                    },
                    Instance = httpContext.Request.Method
                },
                OperationMessage = exception.Message,
                OperationStatus = ResponseMessageStatusEnum.Unauthorized,
                StatusCode = System.Net.HttpStatusCode.Unauthorized
            }
            );
        }

        private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
        {
            var exception = (ForbiddenAccessException)ex;
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            await httpContext.Response.WriteAsJsonAsync(
               new ResponseVM
               {
                   Error = new ProblemDetails
                   {
                       Status = StatusCodes.Status403Forbidden,
                       Type = ex.GetType().Name,
                       Title = "Forbidden Exception",
                       Detail = exception.Message,
                       Extensions =
                       {
                      ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                       },
                       Instance = httpContext.Request.Method
                   },
                   OperationMessage = exception.Message,
                   OperationStatus = ResponseMessageStatusEnum.Forbidden,
                   StatusCode = System.Net.HttpStatusCode.Forbidden
               }
               );
        }

        private async Task HandleBadRequestException(HttpContext httpContext, Exception ex)
        {
            var exception = (Exception)ex;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(
            new ResponseVM
            {
                Error = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = ex.GetType().Name,
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Extensions =
                    {
                   ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                    },
                    Instance = httpContext.Request.Method
                },
                OperationMessage = exception.Message,
                OperationStatus = ResponseMessageStatusEnum.Failure,
                StatusCode = System.Net.HttpStatusCode.BadRequest
            }
            );
        }
        private async Task HandleGlobalException(HttpContext httpContext, Exception ex)
        {
            var exception = (Exception)ex;

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
            new ResponseVM
            {
                Error = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = ex.GetType().Name,
                    Title = "Internal Server Error",
                    Detail = exception.Message,
                    Extensions =
                    {
                   ["traceId"]= Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier
                    },
                    Instance = httpContext.Request.Method
                },
                OperationMessage = exception.Message,
                OperationStatus = ResponseMessageStatusEnum.Exception,
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            }
            );
           
        }
    }
}
