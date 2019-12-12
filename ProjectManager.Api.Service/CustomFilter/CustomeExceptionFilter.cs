using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ProjectManager.Api.Service.CustomFilter
{
    public class CustomeExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string exceptionMessage = string.Empty;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                exceptionMessage = actionExecutedContext.Exception.Message;
            }
            else
            {
                exceptionMessage = actionExecutedContext.Exception.InnerException.Message;
            }

            var respose = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("A unhandel exception was throw by service")
            };
            actionExecutedContext.Response = respose;
        }
    }
}