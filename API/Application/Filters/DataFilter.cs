using Blogpost.API.Application.Enum;
using Blogpost.API.Area.Application;
using Blogpost.Core.Application;
using Blogpost.Core.Application.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogpost.API.Application.Filters
{
    public class DataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            var controller = SetController(context.Controller);
            if (controller == null) return;


            context.HttpContext.Items.Add("_CurrentController", controller);
            SetMethodType(context, controller);
            SetUserContextInController(controller, context);

            if (AllowAnonymousExtension.IsActionAllowedForAnonymous(context.ActionDescriptor as ControllerActionDescriptor))
                return;

            var sessionContext = (ISessionContext)context.HttpContext.RequestServices.GetService(typeof(ISessionContext));

            if (sessionContext == null || sessionContext.UserSession == null)
            {
                throw new UnAuthorizedAccessException("Unauthorized Request ");
            }
            if (controller.MethodType == HttpMethodType.Get || controller.MethodType == HttpMethodType.Delete)
                return;

            if (context.ActionArguments == null)
                return;

            controller.SetActionArguments(context.ActionArguments);

            if (!context.ModelState.IsValid)
            {
                controller.SetValidationResult(context.ModelState);
                throw new Exception("Validation Failure Exception");
            }

            foreach (var argument in context.ActionArguments)
            {
                FillActionAttribute(argument.Value, controller);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = SetController(context.Controller);
            if (controller == null) return;

            if (context.Exception == null && context.Result != null && context.Result is ObjectResult)
            {
                var objResult = context.Result as ObjectResult;
                var data = controller.SetData(objResult.Value);


                context.Result = new ObjectResult(data);
            }

        }

        private void SetMethodType(ActionExecutingContext actionContext, ApiControllerBase controller)
        {
            var method = actionContext.HttpContext.Request.Method.ToLower();

            switch (method)
            {
                case HttpMethodType.Get:
                    controller.SetResponseModel(HttpMethodType.Get);
                    break;
                case HttpMethodType.Delete:
                    controller.SetResponseModel(HttpMethodType.Delete);
                    break;
                case HttpMethodType.Post:
                    controller.SetPostResponseModel(HttpMethodType.Post);
                    break;
                case HttpMethodType.Put:
                    controller.SetPostResponseModel(HttpMethodType.Put);
                    break;
            }
        }

        private ApiControllerBase SetController(object actionController)
        {
            var controller = (actionController as ApiControllerBase);
            if (controller == null)
                return null;

            return controller;
        }


        private void FillActionAttribute(object actionArgument, ApiControllerBase controller)
        {
            if (actionArgument == null) return;


            foreach (var item in actionArgument.GetType().GetProperties()
                        .Where(x => !x.PropertyType.IsValueType && !x.PropertyType.IsPrimitive && !x.PropertyType.FullName.StartsWith("System.")).ToArray())
            {
                FillActionAttribute(item.GetValue(actionArgument), controller);
            }
        }



        private void SetUserContextInController(ApiControllerBase controller, ActionExecutingContext context)
        {
            string cookieValue = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            controller.UserSession = ((ISessionFactory)context.HttpContext.RequestServices.GetService(typeof(ISessionFactory))).GetActiveSessionModel(cookieValue);

            var sessionContext = (ISessionContext)context.HttpContext.RequestServices.GetService(typeof(ISessionContext));
            sessionContext.UserSession = controller.UserSession;
            sessionContext.SessionId = cookieValue;

        }


        public class CustomModelBinder : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                throw new NotImplementedException();
            }
        }


    }

}
