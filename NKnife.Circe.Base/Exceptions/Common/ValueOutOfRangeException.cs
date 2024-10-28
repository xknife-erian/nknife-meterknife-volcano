using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Common
{
    public class ValueOutOfRangeException : AppInternalException
    {
        /// <inheritdoc />
        public ValueOutOfRangeException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept)
        {
        }

        /// <inheritdoc />
        public ValueOutOfRangeException(int number, Exception? innerExcept) : base(number, innerExcept)
        {
        }

        /// <inheritdoc />
        public ValueOutOfRangeException(int number) : base(number)
        {
        }

        /// <inheritdoc />
        public ValueOutOfRangeException(string message) : base(message)
        {
        }
    }
}
