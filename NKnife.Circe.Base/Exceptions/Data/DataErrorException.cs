using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Data;

public class DataErrorException : AppException
{
    /// <inheritdoc />
    public DataErrorException(int number, string message, Exception? innerExcept) : base(number, message, innerExcept) { }

    /// <inheritdoc />
    public DataErrorException(int number, Exception? innerExcept) : base(number, innerExcept) { }

    /// <inheritdoc />
    public DataErrorException(int number) : base(number) { }

    /// <inheritdoc />
    public DataErrorException(string message) : base(message) { }
}