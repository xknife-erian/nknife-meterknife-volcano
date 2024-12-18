using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKnife.Circe.Base.Modules.Managers;
using RAY.Common.Services;

namespace NKnife.Module.UutManager.Internal
{
    public class DefaultUutManager : IUutManager
    {
        #region Implementation of IDisposable
        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Implementation of IManager
        /// <inheritdoc />
        public bool IsLaunched { get; }

        /// <inheritdoc />
        public string? Description { get; }

        /// <inheritdoc />
        public IManager Initialize(params object[] args)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IManager> LaunchAsync(params object[] args)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Stop()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
