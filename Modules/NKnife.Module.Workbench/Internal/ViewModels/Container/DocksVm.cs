using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using RAY.Windows.Common.ViewModels;
using RAY.Windows.Common.ViewModels.Layout;

namespace NKnife.Module.Workbench.Internal.ViewModels.Container
{
    public class DocksVm : BaseViewModelV1
    {
        private readonly ModuleContext _context;
        private bool _isBusy;

        public DocksVm(ModuleContext context)
        {
            _context                    =  context;
            Documents.CollectionChanged += Documents_OnCollectionChanged!;
        }

        public ICommand DocksLoadedCommand => new AsyncRelayCommand(DocksLoadedAsync);

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ObservableCollection<BaseDocumentViewModelV1> Documents { get; } = new();
        public ObservableCollection<BaseToolPaneViewModelV1> Tools { get; } = new();

        public BaseLayoutItemViewModel? ActivePaneViewModel
        {
            get;
            set => SetProperty(ref field, value);
        }

        private Task DocksLoadedAsync()
        {
            return Task.CompletedTask;
        }

        public event EventHandler? DocumentActivated;

        internal virtual void ActivateDocument(BaseDocumentViewModelV1 documentVm)
        {
            if(!Documents.Contains(documentVm))
            {
                Documents.Add(documentVm);
            }

            ActivePaneViewModel = documentVm;
            DocumentActivated?.Invoke(this, EventArgs.Empty);
        }

        internal virtual void ActivateTool(BaseToolPaneViewModelV1 toolVm)
        {
            if(!Tools.Contains(toolVm))
            {
                Tools.Add(toolVm);
            }

            ActivePaneViewModel = toolVm;
        }

        /// <summary>
        ///     判断指定的类型是否需要单例----需要同步Modules中的判断
        /// </summary>
        private bool IsNotSingleInstance(Type type)
        {
            return false;
        }

        #region Event
        private void Documents_OnCollectionChanged(object _, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    if(e.NewItems != null)
                        foreach (var item in e.NewItems)
                            if(item is BaseDocumentViewModelV1 document)
                                document.PropertyChanged += DocumentViewModel_PropertyChanged!;

                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    if(e.OldItems != null)
                        foreach (var item in e.OldItems)
                            if(item is BaseDocumentViewModelV1 document)
                                document.PropertyChanged -= DocumentViewModel_PropertyChanged!;

                    break;
                }
            }
        }

        private void DocumentViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var document = sender as BaseDocumentViewModelV1;

            if(e.PropertyName == nameof(BaseDocumentViewModelV1.IsClosed))
            {
                if(document is { CanClose: true })
                {
                    Documents.Remove(document);
                }
            }
        }
        #endregion
    }
}