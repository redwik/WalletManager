using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WalletManager.Api.Models;
using WalletManager.Domain.Exceptions;

namespace WalletManager.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;

            switch (exception)
            {
                case WalletAlreadyRegisteredException _:
                case PlayerAlreadyExistsException _:
                    code = HttpStatusCode.Conflict;
                    break;
                case PlayerNotFoundException _:
                case WalletNotFound _:
                    code = HttpStatusCode.NotFound;
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            var error = new ErrorModel
            {
                Code = (int)code,
                Description = exception.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
