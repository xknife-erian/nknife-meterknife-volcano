using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NKnife.Feature.ExperimentManager.Internal.View
{
    /// <summary>
    /// ExperimentManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExperimentManagerWindow : Window
    {
        public ExperimentManagerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体拖拽
        /// </summary>
        private void OnMouseDownToDragMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
