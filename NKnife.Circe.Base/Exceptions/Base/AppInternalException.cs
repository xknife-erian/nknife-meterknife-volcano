namespace NKnife.Circe.Base.Exceptions.Base;

/// <summary>
/// 应用程序内部实现导致的错误
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
