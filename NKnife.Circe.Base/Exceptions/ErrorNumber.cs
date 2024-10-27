using System.ComponentModel;

namespace NKnife.Circe.Base.Exceptions
{
    public class ErrorNumber
    {
        /// <summary>
        ///     未定义
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static int TBD => 0;

        /// <summary>
        ///     内部异常 10-999
        /// </summary>
        public struct Internal
        {
            /// <summary>
            ///     日志组件内部异常
            /// </summary>
            public static int NLogError => 30;

            /// <summary> 内部实现异常 </summary>
            public static int InternalAppImplementationError => 100;

            /// <summary> 程序内部数据转换错误 </summary>
            public static int InternalDataConversionError => 101;

            /// <summary> 值超出范围错误 </summary>
            public static int ValueOutOfRangeError => 102;

            [DisplayName("未支持的枚举类型操作")]
            public static int UnsupportedOperationOnEnumType => 103;

            [DisplayName("数据尝试克隆操作时异常")]
            public static int DeepCloneError => 104;
        }

        public class Database
        {
            public static int UnableToConnectToDatabase => 1000;
        }
    }
}
