using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Experiment;
public class ExpFlowErrorException : AppException
{
    /// <inheritdoc />
    public ExpFlowErrorException(int number, string message, Exception? innerExcept = null) : base(number, message, innerExcept) { }

    /// <inheritdoc />
    public ExpFlowErrorException(int number, Exception? innerExcept) : base(number, innerExcept) { }

    /// <inheritdoc />
    public ExpFlowErrorException(int number) : base(number) { }

    /// <inheritdoc />
    public ExpFlowErrorException(string message) : base(message) { }
}

