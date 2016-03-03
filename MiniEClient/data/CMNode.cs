using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using minie.irpc;

namespace MiniEClient.data
{
    public class CMNodeCategory
    {
        public string name { get; set; }
        public int value { get; set; }
    }
    //public class cm_node_rpc : ICloneable
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string desc { get; set; }
    //    public string address { get; set; }
    //    public int category { get; set; }
    //    public cm_node_rpc parent { get; set; }
    //    public List<cm_node_rpc> children;
    //    public sys_user_rpc creator { get; set; }
    //    public DateTime creation_time { get; set; }

    //    public cm_node_rpc(cm_node_rpc parent = null)
    //    {
    //        this.parent = parent;
    //        children = new List<cm_node_rpc>();
    //        if (this.parent != null)
    //            parent.children.Add(this);
    //    }
    //    public object Clone()
    //    {
    //        return this.MemberwiseClone();
    //    }
    //}
    public class CMNode : PNTreeViewItem
    {
        private cm_node_rpc _data;
        public override PNItemType Type
        {
            get
            {
                // TODO:
                if (_data.category.id == 100)
                    return PNItemType.LEAF;
                if (_data.category.id != 900)
                    return PNItemType.BOLE;
                return PNItemType.BOLE | PNItemType.NODELETE | PNItemType.NOEDIT;
            }
        }


        public override string Id { get { return _data.id; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Tips { get { return _data.desc; } }
        public override object Data { get { return _data; } }

        public override string Editor { get { return "CMNodeEditor"; } }

        public CMNode(cm_node_rpc data, PNTreeViewItem parent = null) : base(parent)
        {
            _data = data;
            CMNode tmp;
            foreach (var element in data.children)
            {
                tmp = new CMNode(element, this);
            }
        }

        public override void UpdateData(object data)
        {
            var d = data as cm_node_rpc;
            _data.name = d.name;
            _data.desc = d.desc;
            _data.address = d.address;
            _data.parent = d.parent;
            _data.category = d.category;
        }

        public override object CloneData()
        {
            return _data.Clone();
        }
    }
}
