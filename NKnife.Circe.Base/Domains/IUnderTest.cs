using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAY.Storages;

namespace NKnife.Circe.Base.Domains
{
    /// <summary>
    /// 标记一个正在测试中物件的接口
    /// </summary>
    public interface IUnderTest : IRdbRecord<int>
    {
    }
}
