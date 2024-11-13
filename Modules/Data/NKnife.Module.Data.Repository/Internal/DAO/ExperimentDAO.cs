using LiteDB;
using NKnife.Circe.Base.Modules.Service;
using NKnife.Circe.Entities;
using NKnife.Module.Data.Repository.Internal.Repo.Experiments;
using RAY.Common.Flow;

namespace NKnife.Module.Data.Repository.Internal.DAO
{
    public class ExperimentDao : BaseAppData
    {
        private readonly LiteDatabase _database;
        #region Overrides of BaseAppData
        /// <inheritdoc />
        public override string ConnectingString { get; set; }
        #endregion
        public sealed override string DatabasePath { get; protected set; }

        #region Ctors
        public ExperimentDao(IAppWorkspaceService appWorkspace, ExperimentFileInfo fileInfo)
        {
            DatabasePath = fileInfo.ExperimentFileCacheFullPath();
            ConnectingString = BuildConnectionString(DatabasePath, fileInfo.Password);
            _database = new LiteDatabase(ConnectingString);

            ExperimentRepo = new(_database);
        }
        #endregion

        public ExperimentRepo<Experiment> ExperimentRepo { get; set; }

        public void Close()
        {
            _database.Dispose();
            DbFactory.DisposeDatabase(ConnectingString);
        }

        #region Overrides of BaseAppData
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            Close();
        }
        #endregion

        public override LiteDatabase GetDatabase()
        {
            return _database;
        }
    }
}