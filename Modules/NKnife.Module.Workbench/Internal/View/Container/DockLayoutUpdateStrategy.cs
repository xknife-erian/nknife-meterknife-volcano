using System.Linq;
using System.Windows;
using AvalonDock.Layout;

namespace NKnife.Module.Workbench.Internal.View.Container
{
    /// <summary>
    /// 自定义 AvalonDock 布局更新时的行为，满足特定的需求。
    /// 2024-12-19补充说明：这个类是一个自定义的布局更新策略，用于在初始化布局时，将工具窗口放置在指定的位置。
    /// lukan.
    /// </summary>
    internal class DockLayoutUpdateStrategy : ILayoutUpdateStrategy
    {
        /// <summary>
        /// 在插入可停靠窗口之前调用。可以在这里设置可停靠窗口的默认尺寸，并将其添加到合适的容器中。
        /// </summary>
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorable, ILayoutContainer container)
        {
            // 如果目标容器是浮动窗口，则不处理
            if (container is LayoutAnchorablePane destPane && destPane.FindParent<LayoutFloatingWindow>() != null)
                return false;

            // 设置可停靠窗口的默认尺寸
            SetDefaultAnchorableSize(anchorable);

            // 查找工具窗格并插入可停靠窗口
            var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
            if (toolsPane != null)
            {
                toolsPane.Children.Add(anchorable);
            }
            else
            {
                CreateAndInsertNewPane(layout, anchorable);
            }

            return true;
        }

        /// <summary>
        /// 在插入可停靠窗口之后的处理逻辑
        /// </summary>
        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
            // 插入可停靠窗口之后的处理逻辑
        }

        /// <summary>
        /// 在插入文档之前的处理逻辑
        /// </summary>
        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer)
        {
            // 插入文档之前的处理逻辑
            return false;
        }

        /// <summary>
        /// 在插入文档之后的处理逻辑
        /// </summary>
        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
            // 插入文档之后的处理逻辑
        }

        /// <summary>
        /// 设置可停靠窗口的默认尺寸
        /// </summary>
        private void SetDefaultAnchorableSize(LayoutAnchorable anchorable)
        {
            anchorable.AutoHideWidth = 256;
            anchorable.FloatingWidth = 256;
            anchorable.AutoHideHeight = 128;
        }

        /// <summary>
        /// 创建并插入新的工具窗格
        /// </summary>
        private void CreateAndInsertNewPane(LayoutRoot layout, LayoutAnchorable anchorable)
        {
            var pane = new LayoutAnchorablePane() { DockHeight = new GridLength(300), DockMinWidth = 300 };
            pane.Children.Add(anchorable);
            layout.RootPanel.Children.Insert(0, pane);
        }
    }
}
