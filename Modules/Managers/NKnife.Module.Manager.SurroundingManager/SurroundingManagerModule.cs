﻿using NKnife.Circe.Base.Modules;
using NKnife.Circe.Base.Modules.Manager;
using NKnife.Module.Manager.SurroundingManager.Internal;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Manager.SurroundingManager
{
    public class SurroundingManagerModule : BasePicoModule<IAppWorkspaceManager>
    {
        public override Lazy<IAppWorkspaceManager> Build(params object[] args)
        {
            return new Lazy<IAppWorkspaceManager>(() => new DefaultAppWorkspaceManager());
        }   
        
        public override Task<bool> StartServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override void Dispose()
        {
        }
    }
}
