using System;

namespace Sporthall.Core.Exceptions
{
    public static class ExceptionExtensions
    {
        public static void WalkThroughInnerExceptions(this Exception exception, Action<Exception> exceptionAction)
        {
            exceptionAction(exception);

            if (exception.InnerException != null)
            {
                WalkThroughInnerExceptions(exception.InnerException, exceptionAction);
            }
        }
    }
}
