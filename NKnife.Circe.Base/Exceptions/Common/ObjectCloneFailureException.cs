using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Common
{
    public class ObjectCloneFailureException : AppInternalException
    {
        /// <inheritdoc />
        public ObjectCloneFailureException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept) { }

        /// <inheritdoc />
        public ObjectCloneFailureException(int number, Exception? innerExcept) : base(number, innerExcept) { }

        /// <inheritdoc />
        public ObjectCloneFailureException(int number) : base((int)number) { }

        /// <inheritdoc />
        public ObjectCloneFailureException(string message) : base(message) { }
    }
}
