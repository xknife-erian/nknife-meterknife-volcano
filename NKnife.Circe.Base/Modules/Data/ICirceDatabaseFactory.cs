using RAY.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.Circe.Base.Modules.Data
{
    /// <summary>
    ///     面向Circe的数据库工厂接口
    /// </summary>
    public interface ICirceDatabaseFactory : IDatabaseFactory
    {
        /// <summary>
        ///     创建Runtime(运行时)数据库
        /// </summary>
        /// <param name="experimentId">实验ID</param>
        IDatabase CreateRuntimeDb(string experimentId);
    }
}
