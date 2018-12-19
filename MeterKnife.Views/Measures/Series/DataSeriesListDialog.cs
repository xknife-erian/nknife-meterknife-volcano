﻿using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using MeterKnife.Interfaces;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.Views.Measures.Series
{
    public partial class DataSeriesListDialog : NKnife.ControlKnife.SimpleForm
    {
        private PlotSeriesStyleSolution _solution = new PlotSeriesStyleSolution();
        private readonly IUserHabits _habits = DI.Get<IUserHabits>();

        public DataSeriesListDialog()
        {
            InitializeComponent();
            ButtonStateManager();
            _ListView.SelectedIndexChanged += (s, e) => ButtonStateManager();
        }

        private void ButtonStateManager()
        {
            _DeleteButton.Enabled = _ListView.SelectedItems.Count > 0;
            _ModifyButton.Enabled = _ListView.SelectedItems.Count > 0;
            if (_habits.SeriesStyleSolutionList.Count <= 0)
            {
                _LoadButton.Enabled = false;
            }
        }

        public PlotSeriesStyleSolution Solution
        {
            get
            {
                _solution.Styles.Clear();
                foreach (ListViewItem item in _ListView.Items)
                {
                    var style = (PlotSeriesStyleSolution.ExhibitSeriesStyle) item.Tag;
                    _solution.Styles.Add(style);
                }
                return _solution;
            }
            set
            {
                _solution = value;
                var i = 1;
                foreach (var style in value.Styles)
                {
                    var listItem = new ListViewItem();
                    ByStyle(style, listItem);
                    listItem.Text = $"{i++}";
                    _ListView.Items.Add(listItem);
                }
            }
        }

        private static void ByStyle(PlotSeriesStyleSolution.ExhibitSeriesStyle style, ListViewItem item)
        {
            item.UseItemStyleForSubItems = false;
            item.Tag = style;
            item.SubItems.Clear();
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.Exhibit.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.SeriesLineStyle.ToString()));
            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.Thickness.ToString(CultureInfo.InvariantCulture)));

            var subItem = new ListViewItem.ListViewSubItem();
            subItem.BackColor = style.SeriesStyle.Color;
            item.SubItems.Add(subItem);

            item.SubItems.Add(new ListViewItem.ListViewSubItem(item, style.SeriesStyle.Offset.ToString(CultureInfo.InvariantCulture)));
        }

        private void _AppendButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new DataSeriesEditorDialog();
            dialog.IgnoreExistsExhibits(_solution);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var style = dialog.SeriesStyle;
                var item = new ListViewItem();
                ByStyle(style, item);
                item.Text = $"{_ListView.Items.Count + 1}";
                _ListView.Items.Add(item);
                _solution.Styles.Add(style); //向方案中添加样式
                _ListView.Select();
                item.Selected = true;
            }
            ButtonStateManager();
        }

        private void _DeleteButton_Click(object sender, System.EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var style = (PlotSeriesStyleSolution.ExhibitSeriesStyle) item.Tag;
            var dr = MessageBox.Show(this, $"确认删除{style.Exhibit}数据的显示样式？", "删除样式",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                if (_solution.Styles.Remove(style))
                {
                    _ListView.Items.Remove(item);
                    for (int i = 1; i <= _ListView.Items.Count; i++)
                    {
                        _ListView.Items[i - 1].Text = $"{i}";
                    }
                }
            }
            ButtonStateManager();
        }

        private void _ModifyButton_Click(object sender, System.EventArgs e)
        {
            var item = _ListView.SelectedItems[0];
            var index = item.Text;
            var dialog = new DataSeriesEditorDialog
            {
                SeriesStyle = (PlotSeriesStyleSolution.ExhibitSeriesStyle) item.Tag
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ByStyle(dialog.SeriesStyle, item);
            }
            item.Text = index;
        }

        private void _AcceptButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _LoadButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new SolutionListDialog(false);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Solution = dialog.Solution;
            }
        }

        private void _SaveButton_Click(object sender, System.EventArgs e)
        {
            var dialog = new SolutionListDialog(true);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SaveSolution(dialog.SolutionName);
            }
            ButtonStateManager();
        }

        private void SaveSolution(string solutionName)
        {
            Solution.SolutionName = solutionName;
            var list = new List<PlotSeriesStyleSolution>(_habits.SeriesStyleSolutionList);
            list.Add(Solution);
            _habits.SeriesStyleSolutionList = list;
        }
    }
}