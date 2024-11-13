using LiteDB;
using NKnife.Circe.Base.Modules.Service;
using NKnife.Module.Data.Repository.Internal.Repo.Experiments;
using RAY.Library.Files;

namespace NKnife.Module.Data.Repository.Internal.DAO
{
    public class ExperimentMgmtDao : BaseAppData
    {
        public const string DB_NAME = "ExpMgmt";

        private readonly LiteDatabase _database;
        public override string ConnectingString { get; set; }

        public ExperimentFileInfoRepo ExperimentFileInfoRepo { get; set; }

        public override LiteDatabase GetDatabase()
        {
            return _database;
        }

        #region Overrides of BaseAppData
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public override void Dispose()
        {
            _database.Checkpoint();
            _database.Commit();

            ExperimentFileInfoRepo.Dispose();

            _database.Dispose();
            DbFactory.DisposeDatabase(ConnectingString);
        }

        public override string DatabasePath { get; protected set; }
        #endregion

        #region Ctors
        public ExperimentMgmtDao(IAppWorkspaceService appWorkspace) : this(appWorkspace,
                                                                           Path.Combine("__appWorkspace.LiteDbMgmtFilePath",
                                                                               $"{DB_NAME}.{RayFileExt.ExpHistoryMgmtExpandedName}"),
                                                                           "__appWorkspace.LiteDbFilePassword") { }

        private ExperimentMgmtDao(IAppWorkspaceService appWorkspace, string path, string password = "")
        {
            ConnectingString = BuildConnectionString(path, password);
            var database = DbFactory.GetDatabase(ConnectingString, out var created);
            _database = database;

            DatabasePath = path;

            ExperimentFileInfoRepo = new ExperimentFileInfoRepo(database);
        }

        public static ExperimentMgmtDao Create(IAppWorkspaceService appWorkspace, string path)
        {
            return new ExperimentMgmtDao(appWorkspace, path);
        }
        #endregion
    }
}