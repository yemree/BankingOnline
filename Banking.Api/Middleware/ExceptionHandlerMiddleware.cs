﻿using Banking.Infrastructure.Exceptions;
using Banking.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Banking.Api.Middleware
{
    public static class ExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;

                    context.Response.ContentType = "application/json";

                    var response = new HttpServiceResponseBase();

                    switch (exception)
                    {
                        case ApiException apiException:

                            context.Response.StatusCode = (int)apiException.ErrorCode;
                            response.Error = new ErrorModel
                            {
                                Code = apiException.ErrorCode,
                                Message = apiException.ErrorMessage,
                                Exception = apiException.ErrorMessage
                            };
                            break;
                        default:

                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                            response.Error = new ErrorModel
                            {
                                Code = HttpStatusCode.InternalServerError,
                                Message = "Something Wrong!",
                                Exception = exception.Message
                            };
                            break;
                    }

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

                });
            });
        }
    }
}
