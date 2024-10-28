namespace NKnife.Circe.Base.Exceptions.Base
{
    public class AppException(int number, string message, Exception? innerExcept = null)
        : Exception(Package(number, message), innerExcept)
    {
        public AppException(int number, Exception? innerExcept) : this(number, string.Empty, innerExcept) { }
        public AppException(int number) : this(number, string.Empty, null) { }
        public AppException(string message) : this(Exceptions.ErrorNumber.TBD, string.Empty, null) { }

        /// <summary>
        ///     应用软件异常编号
        /// </summary>
        public int ErrorNumber { get; set; } = number;

        protected static string Package(int number, string message)
        {
            return string.IsNullOrEmpty(message) ? $"{number}" : $"{number}:{message}";
        }
    }
}