namespace NKnife.Circe.Base.Exceptions.Common
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException() : base() { }
        public ValueOutOfRangeException(string message) : base(message) { }
        public ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
