using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ADASOIdentityServer.AuthServer.Models
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<String>? ErrorMessage { get; set; }

        [JsonIgnore] public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

        [JsonIgnore] public bool IsFail => !IsSuccess;

        [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore] public string? UrlAsCreated { get; set; }

        //static factory method
        public static ServiceResult<T> Success(T Data, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                Data = Data,
                StatusCode = status,
            };
        }

        public static ServiceResult<T> SuccessAsCreated(T Data, string urlAsCreated)
        {
            return new ServiceResult<T>
            {
                Data = Data,
                StatusCode = HttpStatusCode.Created,
                UrlAsCreated = urlAsCreated
            };
        }
        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = errorMessage,
                StatusCode = status,

            };
        }


        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessage = [errorMessage],
                StatusCode = status,
            };
        }


    }
    public class ServiceResult
    {

        public List<String>? ErrorMessage { get; set; }

        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        //static factory method
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                StatusCode = status,
            };
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = errorMessage,
                StatusCode = status,

            };
        }
        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessage = [errorMessage],
                StatusCode = status,
            };
        }
    }
}
