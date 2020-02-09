﻿using System.Windows.Forms;
using NKnife.Win.Quick.Controls;

namespace NKnife.Win.Quick.Menus
{
    public sealed class ViewMenuItem : ToolStripMenuItem
    {
        public ViewMenuItem()
        {
            Text = this.Res("视图(&V)");
        }
    }
}