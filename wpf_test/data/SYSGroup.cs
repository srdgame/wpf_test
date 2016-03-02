using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class sys_group_rpc : ICloneable
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public CMNode node { get; set; }
        public bool is_system { get; set; }
        public List<sys_role_rpc> group_roles { get; set; }
        public List<sys_user_rpc> group_users { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
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
