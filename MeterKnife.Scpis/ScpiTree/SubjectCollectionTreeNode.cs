﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Scpis.ScpiTree
{

    public class SubjectCollectionTreeNode : TreeNode
    {
        private readonly ScpiSubjectCollection _collection;

        public SubjectCollectionTreeNode(ScpiSubjectCollection collection)
            : this(string.Format("{0}{1}", collection.Brand, collection.Name))
        {
            _collection = collection;
        }

        public SubjectCollectionTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-collection";
            SelectedImageKey = "subject-collection";
        }

        public ScpiSubjectCollection GetScpiSubjectCollection()
        {
            return _collection;
        }
    }
}