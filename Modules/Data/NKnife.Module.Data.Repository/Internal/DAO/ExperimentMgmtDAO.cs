﻿using LiteDB;
using NKnife.Circe.Base.Modules.Manager;
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
        public ExperimentMgmtDao(ISurroundingsManager surroundings) : this(surroundings,
                                                                           Path.Combine("surroundings.LiteDbMgmtFilePath",
                                                                               $"{DB_NAME}.{RayFileExt.ExpHistoryMgmtExpandedName}"),
                                                                           "surroundings.LiteDbFilePassword") { }

        private ExperimentMgmtDao(ISurroundingsManager surroundings, string path, string password = "")
        {
            ConnectingString = BuildConnectionString(path, password);
            var database = DbFactory.GetDatabase(ConnectingString, out var created);
            _database = database;

            DatabasePath = path;

            ExperimentFileInfoRepo = new ExperimentFileInfoRepo(database);
        }

        public static ExperimentMgmtDao Create(ISurroundingsManager surroundings, string path)
        {
            return new ExperimentMgmtDao(surroundings, path);
        }
        #endregion
    }
}