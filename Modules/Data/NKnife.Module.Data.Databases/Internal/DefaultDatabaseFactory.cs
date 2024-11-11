using NKnife.Circe.Base.Modules.Data;
using RAY.Storages;

namespace NKnife.Module.Data.Databases.Internal
{
    internal class DefaultDatabaseFactory(ModuleContext __context) : ICirceDatabaseFactory
    {
        /// <inheritdoc />
        public IDatabase Create()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public IDatabase CreateRuntimeDb(string experimentId)
        {
            return new RuntimeSqliteDatabase(__context, experimentId);
        }
    }
}
