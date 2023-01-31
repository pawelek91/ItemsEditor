using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ItemsEditorApi.Middleware
{
    public class ExceptionFilter :  IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;
        public ExceptionFilter(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public void OnException(ExceptionContext context)
        {

            int code = 500;

            if (context.Exception is EntityAlreadyExistsException)
                code = (int)HttpStatusCode.Conflict;

            else if (context.Exception is EntityNotFoundException)
                code = (int)HttpStatusCode.NotFound;

            context.Result = new ContentResult
            {
                StatusCode = code,
                Content = context.Exception.ToString()
            };
        }
    }
}
