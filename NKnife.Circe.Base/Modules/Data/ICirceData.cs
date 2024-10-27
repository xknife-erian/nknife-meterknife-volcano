using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NKnife.Circe.Base.Modules.Data
{
    public interface ICirceData : IDisposable
    {
        string DatabasePath { get; }

        string ConnectingString { get; }

        void UpdateDbVersion(int version);
    }
}
