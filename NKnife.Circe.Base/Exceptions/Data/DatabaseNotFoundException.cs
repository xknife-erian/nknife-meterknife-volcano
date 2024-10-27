using NKnife.Circe.Base.Exceptions.Base;

namespace NKnife.Circe.Base.Exceptions.Data
{
    public class DatabaseNotFoundException : AppException
    {
        public DatabaseNotFoundException(int number, Exception? innerExcept) : base(number, innerExcept)
        {
        }

        public DatabaseNotFoundException(int number) : base(number)
        {
        }

        public DatabaseNotFoundException(string message) : base(message)
        {
        }

        public DatabaseNotFoundException(int number, string message, Exception? innerExcept = null) : base(number, message, innerExcept)
        {
        }
    }
}
