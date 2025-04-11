using System;

namespace DatabaseTransfer.Application.Models
{
    public class Result<T>
    {
        private Result(Exception e)
        {
            Exception = e;
        }

        private Result(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; }

        public bool HasException => Exception != null;

        public Exception Exception { get; }

        public static Result<T> Fail(Exception e)
        {
            return new Result<T>(e);
        }

        public static Result<T> Success(T payload)
        {
            return new Result<T>(payload);
        }

        public static implicit operator bool(Result<T> result)
        {
            return result.HasException;
        }

        public override string ToString()
        {
            return HasException ? $"Exception: {Exception}" : $"{Payload}";
        }
    }
}