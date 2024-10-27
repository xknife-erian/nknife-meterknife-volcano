using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Experiment;

public class ExpErrorException : AppException
{
    /// <inheritdoc />
    public ExpErrorException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept) { }

    /// <inheritdoc />
    public ExpErrorException(int number, Exception? innerExcept) : base(number, innerExcept) { }

    /// <inheritdoc />
    public ExpErrorException(int number) : base(number) { }

    /// <inheritdoc />
    public ExpErrorException(string message) : base(message) { }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"ErrorNumber:[{ErrorNumber}]; {Message.Replace("\r\n", ";").Replace("\n", ";").Replace("\r", ";")}";
    }
}
