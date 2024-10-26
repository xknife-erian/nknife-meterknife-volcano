using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NKnife.Feature.Welcome.Internal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Welcome : Window
    {
        public Welcome()
        {
            InitializeComponent();
            _VersionTextBlock_.Text    = GetVersion();
            _StartupLogTextBlock_.Text = string.Empty;
        }

        private string GetVersion()
        {
            var str     = string.Empty;
            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            if(version != null)
                str = $"Version: {version}";
            return str;
        }

        public void SetModuleContext(Context context)
        {
            var welcome = context.Welcome;
            welcome.WelcomeWorkCompleted += (_, _) => Close();

            var binding = new Binding("StartupMessage") { Source = welcome };
            _StartupLogTextBlock_.SetBinding(TextBlock.TextProperty, binding);
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}