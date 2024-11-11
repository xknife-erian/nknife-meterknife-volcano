using RAY.Storages;
using RAY.Storages.Common;

namespace NKnife.Module.Data.Storages.Internal
{
    internal abstract class BaseRdbInternalRepository<T>() : BaseRdbRepository<T, int>()
        where T : class, IRdbRecord<int> { }
}