using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporthall.Core.Exceptions.ServiceExceptions
{
    public class ServiceException : Exception
    {
        public IEnumerable<ServiceError> Errors { get; }

        public ServiceException(params ServiceError[] errors)
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public override string Message =>
            Errors.Select(a =>
                !string.IsNullOrEmpty(a.Key) ? a.Value + ": " + a.Value : a.Value).
                Aggregate((a, b) => a + "\n" + b) ?? typeof(ServiceException).Name;
    }
}
