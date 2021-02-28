using System;
using System.Collections.Generic;
using Blogpost.Core.Application;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blogpost.API.Application.Filters
{
    public class TransactionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //AppManager.Settings = (ISettings)context.HttpContext.RequestServices.GetService(typeof(Core.Application.ISettings));
            var unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
            unitOfWork.StartTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var unitOfWork = (IUnitOfWork)context.HttpContext.RequestServices.GetService(typeof(IUnitOfWork));
            if (context.Exception != null)
            {
                unitOfWork.Rollback();
            }
            else
            {
                unitOfWork.Commit();
            }
        }
    }
}
