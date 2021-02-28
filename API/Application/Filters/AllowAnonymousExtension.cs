using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;

namespace Blogpost.API.Application.Filters
{
    public static class AllowAnonymousExtension
    {
        public static bool IsActionAllowedForAnonymous(ControllerActionDescriptor actionDescriptor)
        {
            return actionDescriptor.MethodInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(AllowAnonymousAttribute)) || actionDescriptor.ControllerTypeInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(AllowAnonymousAttribute));
        }
    }
    public class RawResponseAttribute : Attribute
    {
        public static bool DoesActionHaveAttribute(ControllerActionDescriptor actionDescriptor)
        {
            return actionDescriptor.MethodInfo.CustomAttributes.Any(ca => ca.AttributeType == typeof(RawResponseAttribute));
        }
    }
}
