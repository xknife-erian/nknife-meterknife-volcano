namespace NKnife.Circe.Base.Exceptions.Base
{
    /// <summary>
    ///     应用程序内部异常，通常是需要在代码中处理的异常
    /// </summary>
    public class AppInternalException : AppException
    {
        /// <inheritdoc />
        public AppInternalException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept) { }

        /// <inheritdoc />
        public AppInternalException(int number, Exception? innerExcept) : base(number, innerExcept) { }

        /// <inheritdoc />
        public AppInternalException(int number) : base(number) { }

        /// <inheritdoc />
        public AppInternalException(string message) : base(message) { }
    }
}