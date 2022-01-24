using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Banking.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode ErrorCode { get; set; }
        public ApiException(string errorMessage, HttpStatusCode errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}
