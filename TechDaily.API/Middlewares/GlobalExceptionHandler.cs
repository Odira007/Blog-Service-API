using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using TechDaily.Common.DTOs.Responses;

namespace TechDaily.API.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            var responseModel = ApiResponse.Failure("Something went wrong. Please try again");

            switch (ex)
            {
                case ArgumentNullException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Message = "Please pass in valid input";
                    break;
                case NullReferenceException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(responseModel);
            await response.WriteAsync(result);
        }
    }
}
