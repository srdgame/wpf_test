using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using minie.irpc;

namespace MiniEClient.data
{
    public class sys_group_rpc_equals : EqualsRegister<sys_group_rpc, sys_group_rpc_equals>
    {
        protected override bool Equals(sys_group_rpc obj1, object obj2)
        {
            var o = obj2 as sys_group_rpc;
            if (o != null)
                return obj1.id == o.id;
            return obj1.Equals(obj2);
        }
    }

    public class SYSGroup : PNTreeViewItem
    {
        private sys_group_rpc _data;
        public override PNItemType Type
        {
            get
            {
                return PNItemType.LEAF | PNItemType.NOADD;
            }
        }


        public override string Id { get { return _data.id; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Tips { get { return _data.desc; } }
        public override object Data { get { return _data; } }

        public override string Editor { get { return "SYSGroupEditor"; } }

        public SYSGroup(sys_group_rpc data, PNTreeViewItem parent = null) : base(parent)
        {
            _data = data;
        }

        public override void UpdateData(object data)
        {
            var d = data as sys_group_rpc;
            _data.name = d.name;
            _data.desc = d.desc;
            _data.node = d.node;
            _data.is_system = d.is_system;
        }

        public override object CloneData()
        {
            return _data.Clone();
        }
    }
}
