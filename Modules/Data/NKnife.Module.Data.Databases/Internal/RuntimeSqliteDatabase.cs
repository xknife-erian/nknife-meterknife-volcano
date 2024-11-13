using System.Data;
using System.Data.SQLite;
using System.Text;
using Dapper;
using NLog;
using RAY.Common.Flow.Runtime;
using RAY.Storages;

namespace NKnife.Module.Data.Databases.Internal
{
    internal class RuntimeSqliteDatabase : IDatabase
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly ModuleContext _context;

        private string _runtimeDataPath = string.Empty;

        public RuntimeSqliteDatabase(ModuleContext context, string experimentId)
        {
            _context = context;
            DbFileManager(experimentId);
        }

        private void DbFileManager(string experimentId)
        {
            var tempDataDir = _context.AppWorkspace.TempPath;
            _runtimeDataPath = Path.Combine(tempDataDir, "Runtime");

            try
            {
                if (Directory.Exists(_runtimeDataPath))
                    Directory.Delete(_runtimeDataPath, true);
                Directory.CreateDirectory(_runtimeDataPath);
            }
            catch (Exception e)
            {
                s_logger.Error(e, $"运行时数据交换目录维护异常：{e.Message}");
            }
            
            var dbFileName = GetSqliteFileNameByExperiment(experimentId);
            DatabaseFile = new FileInfo(Path.Combine(_runtimeDataPath, dbFileName));
#if RELEASE
            Account = (string.Empty, experimentId);
#endif
        }

        /// <inheritdoc/>
        public FileInfo? DatabaseFile { get; private set; }

        /// <inheritdoc/>
        public DatabaseType DatabaseType { get; } = DatabaseType.SQLite;

        /// <inheritdoc/>
        public string? ConnectionString { get; private set; }

        /// <inheritdoc/>
        public string? ServerAddress { get; private set; }

        /// <inheritdoc/>
        public string? DatabaseName { get; private set; }

        /// <inheritdoc/>
        public (string? User, string? Password)? Account { get; private set; }

        /// <inheritdoc />
        public object? Tag { get; set; }

        /// <inheritdoc />
        public IList<string>? ParameterList { get; set; }

        public IDbConnection GetDbConnection()
        {
            var sb = new StringBuilder($"Data Source={DatabaseFile!.FullName};Version=3;");
#if RELEASE
            if(Account.HasValue && !string.IsNullOrEmpty(Account.Value.Password)) 
                sb.Append($"Password={Account.Value.Password};");
#endif
            ConnectionString = sb.ToString();
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            connection.Execute(GetCreateTableSql());
            connection.Close();

            return connection;
        }

        /// <inheritdoc />
        public void InitializeDb(Action<string[]> initMethod)
        {
            var sql = GetCreateTableSql();
            initMethod([sql]);
        }

        private static string GetCreateTableSql()
        {
            var sql = $@"
                    CREATE TABLE IF NOT EXISTS {nameof(CompileFeedback)} (
                        {nameof(CompileFeedback.Id)}            INTEGER PRIMARY KEY AUTOINCREMENT,
                        {nameof(CompileFeedback.StepId)}        TEXT,
                        {nameof(CompileFeedback.ParentStepId)}  TEXT,
                        {nameof(CompileFeedback.Phase)}         TEXT,
                        {nameof(CompileFeedback.Level)}         TEXT,
                        {nameof(CompileFeedback.Feedback)}      TEXT,
                        {nameof(CompileFeedback.CreatedTime)}   DATETIME,
                        {nameof(CompileFeedback.ModifyTime)}    DATETIME,
                        {nameof(CompileFeedback.User)}          TEXT
                    );

                    CREATE TABLE IF NOT EXISTS StructuredRecord (
                        StepId TEXT,
                        StepName TEXT
                    );  ";

            return sql;
        }

        private static string GetSqliteFileNameByExperiment(string expId)
        {
            var file = $"~exp_{expId}";
#if DEBUG
            file = $"{file}.sqlite";
#else
            file = $"{file}.recorder.tmp";
#endif
            return file;
        }
    }
}
