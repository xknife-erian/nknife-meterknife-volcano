using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAY.Common.Provider;

namespace NKnife.Module.Data.DataProvider.Internal
{
    internal class DefaultProviderFactory : IProviderFactory
    {
        public T Get<T>() where T : IProvider
        {
            #region Data Provider
            // if (typeof(T) == typeof(IExperimentDataProvider)
            //     || typeof(IExperimentDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_experimentDataProvider;
            //
            // if (typeof(T) == typeof(IExperimentMgmtDataProvider)
            //     || typeof(IExperimentMgmtDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IDbDataProvider)_expManagerCacheProvider;
            //
            // if (typeof(T) == typeof(IWorkstationDataProvider)
            //     || typeof(IWorkstationDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_instrumentDataProvider;
            //
            // if (typeof(T) == typeof(IUserDataProvider)
            //     || typeof(IUserDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IDbDataProvider)_userDataProvider;
            //
            // if (typeof(T) == typeof(IPipetteDataProvider)
            //     || typeof(IPipetteDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_pipetteDataProvider;
            //
            // if (typeof(T) == typeof(ILabwareDataProvider)
            //     || typeof(ILabwareDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_labwareDataProvider;
            //
            // if (typeof(T) == typeof(IFittingsDataProvider)
            //     || typeof(IFittingsDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_fittingsDataProvider;
            //
            // if (typeof(T) == typeof(IBluePrintDataProvider)
            //     || typeof(IBluePrintDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_blueprintDataProvider;
            //
            // if (typeof(T) == typeof(ILiquidClassDataProvider)
            //     || typeof(ILiquidClassDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(ILiquidClassDataProvider)_liquidClassDataProvider;
            //
            // if (typeof(T) == typeof(IRayLabwareDataProvider)
            //     || typeof(IRayLabwareDataProvider).IsAssignableFrom(typeof(T)))
            //     return (T)(IFileDataProvider)_rayLabwareDataProvider;
            #endregion

            throw new ArgumentException("Unsupported provider type");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
