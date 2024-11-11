using System.Collections.Concurrent;
using RAY.Common.Authentication;
using RAY.Common.Flow;

namespace NKnife.Circe.Entities
{
    public class Experiment : IExperiment
    {
        #region Implementation of IVariety
        /// <inheritdoc />
        public string VarietyId { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }
        #endregion
        #region Implementation of IUnique
        /// <inheritdoc />
        public string? UniqueCode { get; }

        /// <inheritdoc />
        public void CreateInstance()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Implementation of IAuditable
        /// <inheritdoc />
        public DateTime CreatedTime { get; set; }

        /// <inheritdoc />
        public IUser? Creator { get; set; }

        /// <inheritdoc />
        public DateTime ModifyTime { get; set; }

        /// <inheritdoc />
        public IUser? Modifier { get; set; }
        #endregion
        #region Implementation of IVersionedRecord
        /// <inheritdoc />
        public int Version { get; set; }
        #endregion
        #region Implementation of IExperiment
        /// <inheritdoc />
        public Action? AfterStopped { get; }

        /// <inheritdoc />
        public bool IsTemplate { get; set; }

        /// <inheritdoc />
        public IGroupStep Flow { get; set; }

        /// <inheritdoc />
        public ConcurrentDictionary<int, TaskCompletionSource<bool>> SignalSlots { get; set; }

        /// <inheritdoc />
        public event EventHandler? Disposing;
        #endregion
        #region Implementation of IDisposable
        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}