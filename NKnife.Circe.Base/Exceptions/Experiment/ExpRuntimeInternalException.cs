using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Experiment
{
    internal class ExpRuntimeInternalException : AppInternalException
    {
        /// <inheritdoc />
        public ExpRuntimeInternalException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept)
        {
        }

        /// <inheritdoc />
        public ExpRuntimeInternalException(int number, Exception? innerExcept) : base(number, innerExcept)
        {
        }

        /// <inheritdoc />
        public ExpRuntimeInternalException(int number) : base(number)
        {
        }

        /// <inheritdoc />
        public ExpRuntimeInternalException(string message) : base(message)
        {
        }
    }
}