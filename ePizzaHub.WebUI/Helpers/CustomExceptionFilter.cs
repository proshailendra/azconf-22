using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ePizzaHub.WebUI.Helpers
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string msg = context.Exception.Message;
            context.Result = new ViewResult { ViewName = "Error" };
            context.ExceptionHandled = true;
        }
    }
}