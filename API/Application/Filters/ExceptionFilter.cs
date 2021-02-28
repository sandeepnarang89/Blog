using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Blogpost.API.Area.Application;
using Blogpost.Core.Application.Exception;
using System.Net;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Blogpost.API.Application.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var controller = context.HttpContext.Items["_CurrentController"] as ApiControllerBase;

            if (controller == null)
                return;


            if (context.Exception is BaseException)
            {
                controller.ResponseModel.SetErrorMessage(context.Exception.Message);
            }
            if (!RawResponseAttribute.DoesActionHaveAttribute(context.ActionDescriptor as ControllerActionDescriptor))
            {
                controller.ResponseModel.StatusCode = GetExceptionCode(context);
                var result = new ObjectResult(controller.ResponseModel);
                result.StatusCode = controller.ResponseModel.StatusCode;
                context.Result = result;
            }

        }


        private int GetExceptionCode(ExceptionContext context)
        {
            if (context.Exception is ValidationFailureException || context.Exception is InvalidDataProvidedException)
            {
                return (int)HttpStatusCode.BadRequest;

            }
            else if (context.Exception is MemberAccessException || context.Exception is UnAuthorizedAccessException)
            {
                return (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }

}
