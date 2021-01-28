using System;

namespace ASP.NET_Core_Web_APIs
{
    public sealed class OperationResult<T>
    {
        private readonly T _result;


        public bool IsSuccessful { get; }

        public string Message { get; }

        public T Result
        {
            get
            {
                if (!IsSuccessful)
                {
                    throw new InvalidOperationException();
                }
                return _result;
            }
        }


        private OperationResult(bool isSuccessful, T result, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            _result = result;
        }


        public static OperationResult<T> CreateUnsuccessful(string errorMessage)
        {
            return new OperationResult<T>(false, default(T), errorMessage);
        }

        public static implicit operator OperationResult<T>(T result)
        {
            return CreateSuccessful(result);
        }


        private static OperationResult<T> CreateSuccessful(T result, string message = default)
        {
            return new OperationResult<T>(true, result, message);
        }
    }

    public static class OperationResult
    {
        public static OperationResult<T> CreateSuccessful<T>(T result)
        {
            return result;
        }
    }
}
