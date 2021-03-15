using System;
using System.Net;
using System.Net.Http;

namespace MiniTwitApi.Client.Models
{
    public static class HttpFailureHelper
    {
        public static void HandleStatusCode(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException("You are not authorized");
                case HttpStatusCode.BadRequest:
                    throw new Exception($"You have provided wrong information: {response.ReasonPhrase}");
                case HttpStatusCode.NotFound:
                    throw new Exception("The resource is not found");
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException("You are not authorized");
                case HttpStatusCode.MethodNotAllowed:
                    break;
                case HttpStatusCode.NotAcceptable:
                    break;
                case HttpStatusCode.RequestTimeout:
                    break;
                case HttpStatusCode.Conflict:
                    break;
                case HttpStatusCode.InternalServerError:
                    break;
                case HttpStatusCode.NotImplemented:
                    break;
                case HttpStatusCode.BadGateway:
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    break;
            }
        } 
    }
}