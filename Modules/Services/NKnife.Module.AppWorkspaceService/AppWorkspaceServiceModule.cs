﻿using NKnife.Circe.Base.Modules.Services;
using NKnife.Module.AppWorkspaceService.Internal;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.AppWorkspaceService
{
    public class AppWorkspaceServiceModule : BasePicoModule<IAppWorkspaceService>
    {
        public override Lazy<IAppWorkspaceService> Build(params object[] args)
        {
            return new Lazy<IAppWorkspaceService>(() => new DefaultAppWorkspaceService());
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
