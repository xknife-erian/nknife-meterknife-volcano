using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Circe.Base.Domains;

namespace NKnife.Circe.Entities
{
    /// <summary>
    /// UUT，是unit under test的缩写，意思是被测试单元(模块)。<br/>
    /// https://baike.baidu.com/item/UUT/10559232 <br/>
    /// https://baike.baidu.com/item/%E8%A2%AB%E6%B5%8B%E5%99%A8%E4%BB%B6/22762794
    /// </summary>
    public class UUT : IUnderTest
    {
        #region Implementation of IRdbRecord<int>.Id
        /// <inheritdoc />
        public int Id { get; set; }
        #endregion
        
        #region Implementation of ITimeAuditable
        /// <inheritdoc />
        public DateTime CreatedTime { get; set; }

        /// <inheritdoc />
        public DateTime ModifyTime { get; set; }
        #endregion

    }

    public class Connector
    {

    }
}
