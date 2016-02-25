﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class CMNodeCategory
    {
        public string name { get; set; }
        public int value { get; set; }
    }
    public class CMNodeBase : NodeData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string address { get; set; }
        public int category { get; set; }
        public string parent { get; set; }
        public string creator { get; set; }
        public DateTime creation_time { get; set; }

        public override NodeType Type
        {
            get
            {
                // TODO:
                if (category == 100)
                    return NodeType.LEAF;
                if (category != 900)
                    return NodeType.BOLE;
                return NodeType.BOLE | NodeType.NODELETE | NodeType.NOEDIT;
            }
        }

        public override string Id { get { return id; } }
        public override string DisplayName { get { return name; } }
        public override string Tips { get { return desc; } }

        public CMNodeBase(CMNodeBase parent = null) : base(parent)
        {
            if (parent != null)
                this.parent = parent.id;
        }
    }
}
