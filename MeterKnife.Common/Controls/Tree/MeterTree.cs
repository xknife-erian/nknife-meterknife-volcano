using System;
using System.Drawing;
using System.Windows.Forms;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Properties;

namespace MeterKnife.Common.Controls.Tree
{
    public sealed class MeterTree : TreeView
    {
        private int _MouseClicks; //��¼�����TreeView�ؼ��ϰ��µĴ���

        public TreeNode RootNode
        {
            get { return Nodes[0]; }
        }

        public MeterTree()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            ImageList = GetImageList();
            ShowLines = false;
            FullRowSelect = true;

            //�Զ�����ƽڵ���ı���ͼ��
            //DrawMode = TreeViewDrawMode.OwnerDrawText;
            //DrawNode += Tree_DrawNode;

            //ͨ�������ֶθı�ڵ���м�࣬ʹ�ø�����һЩ
            //const int TV_FIRST = 0x1100;
            //const int TVM_SETITEMHEIGHT = TV_FIRST + 27;
            //API.User32.SendMessage(Handle, TVM_SETITEMHEIGHT, 20, 0);
            ItemHeight = 23;

            //ͨ�������ֶ�ʹ�ý��㶪ʧʱ��ѡ�еĽڵ����и�����ʾ
            Leave += ProjectTree_Leave;
            BeforeSelect += ProjectTree_BeforeSelect;

            //Microsoft��TreeView�ؼ����������ŵ�����˫���ڵ�ʱ�Զ�չ��/�۵��ڵ㡣
            //�������Զ���NodeMouseDoubleClick�¼������ǣ�ͬʱ�ֲ�ϣ���ı����չ��/�۵�״̬�����޷�ֱ�Ӵﵽ��һЧ����
            //ͨ�������ֶ����߾ȹ���
            MouseDown += (s, e) => { _MouseClicks = e.Clicks; };
            BeforeCollapse += (s, e) => { e.Cancel = _MouseClicks > 1; };
            BeforeExpand += (s, e) => { e.Cancel = _MouseClicks > 1; };

            //���еĽڵ���Ϸ����
            //AllowDrop = true;
            //ItemDrag += OnItemDrag;
            //DragEnter += OnDragEnter;
            //DragOver += OnDragOver;
            //DragDrop += OnDragDrop;

            Nodes.Add(new PCNode());
        }

        private void TreeDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Rectangle nodeRect = e.Node.Bounds; //�ڵ�����

            Point drawPt = new Point(nodeRect.Location.X - 18, nodeRect.Location.Y); //����ͼ�����ʼλ��
            Size imgSize = new Size(12, 12); //ͼƬ��С
            Rectangle imgRect = new Rectangle(drawPt, imgSize);

            //--------����ͼƬ: �жϽڵ����ͣ������ݸ��ڵ�����ͻ��Ʋ�ͬ��ͼƬ--------------------
            if (e.Node is BaseTreeNode)
            {
                //this.LegendIcon.Draw(e.Graphics, drawPt, 0);
            }

            //-----------------------�����ı� -------------------------------
            Font nodeFont = e.Node.NodeFont ?? ((TreeView) sender).Font;
            Brush textBrush = SystemBrushes.WindowText;
            //��ɫͻ����ʾ
            if ((e.State & TreeNodeStates.Focused) != 0)
                textBrush = SystemBrushes.Window;
            //���޶��ı��������������ʱ���ı�����ȡ----edited by: Vivi 2009/11/19
            e.Graphics.DrawString(e.Node.Text, nodeFont, textBrush, Rectangle.Inflate(nodeRect, -5, -5));
        }

        #region �ڵ���Ϸ����

        private TreeNode _LastNodeOnDrag;//����ǰһ��������Ľڵ�

        private void OnItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item != null)
                //�����Ϸ�����Ϊ�ƶ�
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            //��ȡ���϶��Ľڵ�
            var draggingNode = FindDragNode(e.Data);
            //�޸��������Ŀ��ڵ�ı���ɫ����ԭ��һ���ڵ�ı���ɫ
            var targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if ((targetNode != null) && (targetNode != _LastNodeOnDrag))
            {
                targetNode.BackColor = Color.PaleTurquoise;
                _LastNodeOnDrag.BackColor = Color.White;
                _LastNodeOnDrag = targetNode;
                e.Effect = (CanDropNode(draggingNode, targetNode)) ? DragDropEffects.Move : DragDropEffects.None;
            }
        }

        /// <summary>
        /// �ж�Ŀ��ڵ��Ƿ���Խ��ձ��϶��Ľڵ�
        /// </summary>
        /// <param name="draggingNode">���϶��Ľڵ�</param>
        /// <param name="targetNode">Ŀ��ڵ�</param>
        /// <returns></returns>
        private bool CanDropNode(TreeNode draggingNode, TreeNode targetNode)
        {
            return true;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            //��ȡ���϶��Ľڵ�
            var draggingNode = FindDragNode(e.Data);
            //����ڵ������ݣ��Ϸ�Ŀ�������ƶ�
            e.Effect = (draggingNode != null) ? DragDropEffects.Move : DragDropEffects.None;
            var targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //�ı����ڵ�ı���ɫ
                targetNode.BackColor = Color.Khaki;
                //����˽ڵ㣬������һ��ʱ��ԭ����ɫ
                _LastNodeOnDrag = targetNode;
            }
        }

        private static TreeNode FindDragNode(IDataObject dataObject)
        {
            var types = dataObject.GetFormats(false);
            foreach (var type in types)
            {
                if (!type.Contains("Node"))
                {
                    continue;
                }
                var obj = dataObject.GetData(type);
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
            //���ϷŽ���ʱ
            var node = FindDragNode(e.Data);
            //�õ���ǰ������Ľڵ�
            var targetNode = GetNodeAt(PointToClient(new Point(e.X, e.Y)));
            if (targetNode != null)
            {
                //ɾ���ϷŵĽڵ�
                node.Remove();
                //��ӵ�Ŀ��ڵ�
                targetNode.Nodes.Add(node);
                targetNode.BackColor = Color.White;
                SelectedNode = targetNode;
            }
        }

        #endregion

        private bool _IsTreeLeave = false;

        private void ProjectTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //�ڿ�ʼѡ���µĽڵ�ǰ�����ϴ�ѡ��Ľڵ㱳����Ĭ��״̬
            if (SelectedNode != null && _IsTreeLeave)
            {
                SelectedNode.BackColor = Color.White;
                _IsTreeLeave = false;
            }
        }

        private void ProjectTree_Leave(object sender, EventArgs e)
        {
            //�����㶪ʧʱ����ѡ�еĽڵ����и�����ʾ
            if (SelectedNode != null)
            {
                SelectedNode.BackColor = Color.Gainsboro;
                _IsTreeLeave = true;
            }
        }

        private ImageList GetImageList()
        {
            var il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.ImageSize = new Size(18, 18);

            il.Images.Add(MeterTreeElement.PC, GlobalResources.pc);
            il.Images.Add(MeterTreeElement.Serial, GlobalResources.serial);
            il.Images.Add(MeterTreeElement.Care, GlobalResources.care);
            il.Images.Add(MeterTreeElement.Meter, GlobalResources.meter);
            return il;
        }


    }
}