using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RAY.Windows.Common;

namespace NKnife.Module.UI.MainWorkbench.Internal.ViewModels.Container
{
    public class DocksVm : BaseViewModel
    {
        private ObservableObject? _activePaneViewModel;
        private bool _isBusy;

        public DocksVm()
        {
            Documents.CollectionChanged += Documents_OnCollectionChanged!;
        }

        public ICommand DocksLoadedCommand => new AsyncRelayCommand(DocksLoadedAsync);

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ObservableCollection<BaseViewModel> Documents { get; } = new ();
        public ObservableCollection<BaseViewModel> Tools { get; } = new ();

        public ObservableObject? ActivePaneViewModel
        {
            get => _activePaneViewModel;
            set
            {
                if(SetProperty(ref _activePaneViewModel, value))
                {
                    ActiveChanged(value);
                }
            }
        }

        private Task DocksLoadedAsync()
        {
            return Task.CompletedTask;
        }
        
        public event EventHandler? DocumentActivated;

        /// <summary>
        ///     激活项与Coordinator间的触发
        /// </summary>
        /// <param name="value"></param>
        private void ActiveChanged(ObservableObject? value) { }

        internal virtual void ActivateDocument(BaseViewModel documentVm)
        {
            documentVm.IsClosed = false;

            if(!Documents.Contains(documentVm))
            {
                Documents.Add(documentVm);
            }
            ActivePaneViewModel = documentVm;
            DocumentActivated?.Invoke(this, EventArgs.Empty);
        }


        internal virtual void ActivateTool(BaseViewModel toolVm)
        {
            toolVm.IsVisible = true;

            if(!Tools.Contains(toolVm))
            {
                if(IsNotSingleInstance(toolVm.GetType()))
                {
                    toolVm.IsClosed = false;
                }
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
                            if(item is BaseViewModel document)
                                document.PropertyChanged += DocumentViewModel_PropertyChanged!;

                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    if(e.OldItems != null)
                        foreach (var item in e.OldItems)
                            if(item is BaseViewModel document)
                                document.PropertyChanged -= DocumentViewModel_PropertyChanged!;

                    break;
                }
            }
        }

        private void DocumentViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var document = sender as BaseViewModel;

            if(e.PropertyName == nameof(BaseViewModel.IsClosed))
            {
                if(document is { CanClose: true })
                    Documents.Remove(document);
            }
        }
        #endregion
    }
}