using Dapper;
using RAY.Common.Flow.Runtime;
using RAY.Library.Collection;
using RAY.Storages.Common;

namespace NKnife.Module.Data.Storages.Internal.Repositories
{
    internal class CompileFeedbackRepository : BaseRdbRepository<CompileFeedback, int>, ICompileFeedbackRepository
    {
        /// <inheritdoc />
        public async Task<GeneralTree<CompileFeedback>?> GetAsync(string stepId)
        {
            var rootFeedback = await GetFeedbackByStepIdAsync(stepId);
            if (rootFeedback == null)
            {
                return null;
            }

            var rootTree = new GeneralTree<CompileFeedback>(rootFeedback);
            await BuildTreeAsync(rootTree);
            return rootTree;
        }

        private async Task<CompileFeedback?> GetFeedbackByStepIdAsync(string stepId)
        {
            const string SQL = @"
                SELECT * FROM CompileFeedback
                WHERE StepId = @StepId";

            if(_Connection != null)
                return await _Connection.QueryFirstOrDefaultAsync<CompileFeedback>(SQL, new { StepId = stepId }, _Transaction);
            return null;
        }

        private async Task BuildTreeAsync(GeneralTree<CompileFeedback> parentNode)
        {
            const string SQL = @"
                SELECT * FROM CompileFeedback
                WHERE ParentStepId = @ParentStepId";

            if (_Connection != null)
            {
                var childFeedbacks = await _Connection.QueryAsync<CompileFeedback>(SQL, new { ParentStepId = parentNode.Data.StepId }, _Transaction);

                foreach (var childFeedback in childFeedbacks)
                {
                    var childNode = new GeneralTree<CompileFeedback>(childFeedback);
                    parentNode.Add(childNode);
                    await BuildTreeAsync(childNode);
                }
            }
        }
    }
}
