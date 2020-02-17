namespace Sporthall.Core.Exceptions.ServiceExceptions
{
    public class ServiceError
    {
        public ServiceError(string key, string value, ServiceErrorType errorType, string additionalText = null)
        {
            Key = key;
            Value = value;
            AdditionalText = additionalText;
            ErrorType = errorType;
        }

        public string Key { get; }

        public string Value { get; }

        public string AdditionalText { get; }

        public ServiceErrorType ErrorType { get; }
    }
}
