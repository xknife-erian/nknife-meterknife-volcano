using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.Circe.Base;

namespace NKnife.Module.WelcomeService.Internal
{
    internal class DefaultAppWelcome : ObservableObject, IAppWelcome
    {
        private string _startupMessage = string.Empty;
        private bool _isLoginSuccess;
        
        /// <inheritdoc />
        public void CompleteWelcomeWork()
        {
            WelcomeWorkCompleted?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? WelcomeWorkCompleted;

        public string StartupMessage
        {
            get => _startupMessage;
            set => SetProperty(ref _startupMessage, value);
        }

        public bool IsLoginSuccess
        {
            get => _isLoginSuccess;
            set => SetProperty(ref _isLoginSuccess, value);
        }

        public void StartPauseWelcomeWork()
        {
            PauseWelcomeStarted?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public event EventHandler? PauseWelcomeStarted;
    }
}