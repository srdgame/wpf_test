using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using minie.irpc;

namespace MiniEClient.data
{
    public class cm_node_rpc_equals : EqualsRegister<cm_node_rpc, cm_node_rpc_equals>
    {
        protected override bool Equals(cm_node_rpc obj1, object obj2)
        {
            var o = obj2 as cm_node_rpc;
            if (o != null)
                return obj1.id == o.id;
            return obj1.Equals(obj2);
        }
    }
    public class CMNodeCategory
    {
        private cm_node_category_rpc _data;

        public int id { get { return _data.id; } }
        public string name { get { return _data.desc; } }
        public string desc { get { return _data.desc; } }
        public CMNodeCategory(cm_node_category_rpc data)
        {
            _data = data;
        }
        public override bool Equals(object obj)
        {
            var o = obj as cm_node_category_rpc;
            if (o == null)
                return false;
            return o.id == id;
        }
        public override int GetHashCode()
        {
            return _data.id.GetHashCode();
        }
    }
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
                // FIXME: Hack for correct parent
                if (element.parent == null)
                    element.parent = data;
                tmp = new CMNode(element, this);
            }
        }

        public override void UpdateData(object data)
        {
            var d = data as cm_node_rpc;
            _data.id = d.id;
            _data.name = d.name;
            _data.desc = d.desc;
            _data.address = d.address;
            _data.category = d.category;
            if (_data.parent != d.parent)
            {
                if (_data.parent != null)
                    _data.parent.children.Remove(_data);
                _data.parent = d.parent;
                if (_data.parent != null)
                    _data.parent.children.Add(_data);
            }
        }

        public override object CloneData()
        {
            return _data.Clone();
        }
    }
}
