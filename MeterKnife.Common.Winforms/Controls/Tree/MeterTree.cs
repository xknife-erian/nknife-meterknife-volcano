using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Logging;
using MerterKnife.Common.Winforms.Controls.Tree;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.EventParameters;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Properties;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.Events;
using NKnife.Tunnel;

namespace MeterKnife.Workbench.Controls.Tree
{
    public sealed class MeterTree : TreeView
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterTree>();
        private int _MouseClicks; //记录鼠标在TreeView控件上按下的次数

        public MeterTree()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            ImageList = GetImageList();
            ShowLines = false;
            FullRowSelect = true;

            MouseClick += OnMouseClick;
            DoubleClick += OnMouseDoubleClick;

            //自定义绘制节点的文本和图标
            //DrawMode = TreeViewDrawMode.OwnerDrawText;
            //DrawNode += Tree_DrawNode;

            //通过以下手段改变节点的行间距，使得更美观一些
            //const int TV_FIRST = 0x1100;
            //const int TVM_SETITEMHEIGHT = TV_FIRST + 27;
            //API.User32.SendMessage(Handle, TVM_SETITEMHEIGHT, 20, 0);
            ItemHeight = 23;

            //通过以下手段使得焦点丢失时，选中的节点仍有高亮显示
            Leave += TreeLeave;
            BeforeSelect += TreeBeforeSelect;

            //Microsoft在TreeView控件中自作主张地做成双击节点时自动展开/折叠节点。
            //因已有自定义NodeMouseDoubleClick事件，但是，同时又不希望改变结点的展开/折叠状态，就无法直接达到这一效果。
            //通过以下手段曲线救国。
            MouseDown += (s, e) => { _MouseClicks = e.Clicks; };
            BeforeCollapse += (s, e) => { e.Cancel = _MouseClicks > 1; };
            BeforeExpand += (s, e) => { e.Cancel = _MouseClicks > 1; };

            //树中的节点可拖放设计
            //AllowDrop = true;
            //ItemDrag += OnItemDrag;
            //DragEnter += OnDragEnter;
            //DragOver += OnDragOver;
            //DragDrop += OnDragDrop;

            Nodes.Add(new PCNode());
        }

        public TreeNode RootNode
        {
            get { return Nodes[0]; }
        }

        private void OnMouseClick(object s, MouseEventArgs e)
        {
            //将点击事件传递到子节点去实现
            if (SelectedNode != null)
            {
                if (SelectedNode is InterfaceNode)
                {
                    //当点击接口节点时，不做鼠标点击的判断，直接传递给节点，在节点类中进行判断处理
                    var node = SelectedNode as InterfaceNode;
                    node.OnNodeClicked(e);
                }
            }
        }

        private void OnMouseDoubleClick(object sender, EventArgs e)
        {
            if (SelectedNode is MeterNode)
            {
                //当左键双击仪器节点
                var node = SelectedNode as MeterNode;
                var interfaceNode = (InterfaceNode) node.Parent;
                if (interfaceNode is LanNode)
                {
                    //TODO:interfaceNode.Port.TunnelType = TunnelType.Tcpip;
                }
                _logger.Trace("左键双击仪器节点");
                OnSelectedMeter(new InterfaceNodeClickedEventArgs(node.Meter, interfaceNode.Port));
            }
        }

        public event EventHandler<InterfaceNodeClickedEventArgs> SelectedMeter;

        private void OnSelectedMeter(InterfaceNodeClickedEventArgs e)
        {
            EventHandler<InterfaceNodeClickedEventArgs> handler = SelectedMeter;
            if (handler != null) handler(this, e);
        }

        private ImageList GetImageList()
        {
            var il = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit, 
                ImageSize = new Size(18, 18)
            };

            il.Images.Add(MeterTreeElement.PC, GlobalResources.pc);
            il.Images.Add(MeterTreeElement.Serial, GlobalResources.serial);
            il.Images.Add(MeterTreeElement.Care, GlobalResources.care);
            il.Images.Add(MeterTreeElement.Meter, GlobalResources.meter);
            return il;
        }

        #region 丢焦点时高亮

        private bool _IsTreeLeave;

        private void TreeBeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //在开始选择新的节点前，置上次选择的节点背景回默认状态
            if (SelectedNode != null && _IsTreeLeave)
            {
                SelectedNode.BackColor = Color.White;
                _IsTreeLeave = false;
            }
        }

        private void TreeLeave(object sender, EventArgs e)
        {
            //当焦点丢失时，被选中的节点仍有高亮显示
            if (SelectedNode != null)
            {
                SelectedNode.BackColor = Color.Gainsboro;
                _IsTreeLeave = true;
            }
        }

        #endregion

        #region 自绘制节点

        private void TreeDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle nodeRect = e.Node.Bounds; //节点区域

            var drawPt = new Point(nodeRect.Location.X - 18, nodeRect.Location.Y); //绘制图标的起始位置
            var imgSize = new Size(12, 12); //图片大小
            var imgRect = new Rectangle(drawPt, imgSize);

            //--------绘制图片: 判断节点类型，并根据各节点的类型绘制不同的图片--------------------
            if (e.Node is BaseTreeNode)
            {
                //this.LegendIcon.Draw(e.Graphics, drawPt, 0);
            }

            //-----------------------绘制文本 -------------------------------
            Font nodeFont = e.Node.NodeFont ?? ((TreeView) sender).Font;
            Brush textBrush = SystemBrushes.WindowText;
            //反色突出显示
            if ((e.State & TreeNodeStates.Focused) != 0)
                textBrush = SystemBrushes.Window;
            //不限定文本区域，以免大字体时长文本被截取----edited by: Vivi 2009/11/19
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, Rectangle.Inflate(nodeRect, -5, -5));
        }

        #endregion

        #region 节点可拖放设计

        private TreeNode _LastNodeOnDrag; //保存前一个鼠标进入的节点

        private void OnItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item != null)
                //设置拖放类型为移动
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            //获取被拖动的节点
            TreeNode draggingNode = FindDragNode(e.Data);
            //修改鼠标进入的目标节点的背景色，还原上一个节点的背景色
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if ((targetNode != null) && (targetNode != _LastNodeOnDrag))
            {
                targetNode.BackColor = Color.PaleTurquoise;
                _LastNodeOnDrag.BackColor = Color.White;
                _LastNodeOnDrag = targetNode;
                e.Effect = (CanDropNode(draggingNode, targetNode)) ? DragDropEffects.Move : DragDropEffects.None;
            }
        }

        /// <summary>
        ///     判断目标节点是否可以接收被拖动的节点
        /// </summary>
        /// <param name="draggingNode">被拖动的节点</param>
        /// <param name="targetNode">目标节点</param>
        /// <returns></returns>
        private bool CanDropNode(TreeNode draggingNode, TreeNode targetNode)
        {
            return true;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            //获取被拖动的节点
            TreeNode draggingNode = FindDragNode(e.Data);
            //如果节点有数据，拖放目标允许移动
            e.Effect = (draggingNode != null) ? DragDropEffects.Move : DragDropEffects.None;
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //改变进入节点的背景色
                targetNode.BackColor = Color.Khaki;
                //保存此节点，进入下一个时还原背景色
                _LastNodeOnDrag = targetNode;
            }
        }

        private static TreeNode FindDragNode(IDataObject dataObject)
        {
            string[] types = dataObject.GetFormats(false);
            foreach (string type in types)
            {
                if (!type.Contains("Node"))
                {
                    continue;
                }
                object obj = dataObject.GetData(type);
                var node = obj as TreeNode;
                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            //当拖放结束时
            TreeNode node = FindDragNode(e.Data);
            //得到当前鼠标进入的节点
            TreeNode targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //删除拖放的节点
                node.Remove();
                //添加到目标节点
                targetNode.Nodes.Add(node);
                targetNode.BackColor = Color.White;
                SelectedNode = targetNode;
            }
        }

        #endregion
    }
}