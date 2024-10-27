using LiteDB;
using RAY.Common.Flow;

namespace NKnife.Module.Data.Repository.Internal.Repo.Experiments
{
    public class ExperimentRepo<T> : BaseEntityRepository<T> where T : IExperiment
    {
        public ExperimentRepo(string dbFile) : base(dbFile)
        {
        }

        public ExperimentRepo(LiteDatabase database) : base(database)
        {
        }
    }
}