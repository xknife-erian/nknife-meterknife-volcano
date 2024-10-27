using LiteDB;
using RAY.Common.Flow;

namespace NKnife.Module.Data.Repository.Internal.Repo.Experiments
{
    public class ExperimentFileInfoRepo : BaseEntityRepository<ExperimentFileInfo>
    {
        public ExperimentFileInfoRepo(string dbFile) : base(dbFile)
        {
        }

        public ExperimentFileInfoRepo(LiteDatabase database) : base(database)
        {
        }
    }
}