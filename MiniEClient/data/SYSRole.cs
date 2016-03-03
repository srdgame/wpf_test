using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using minie.irpc;

namespace MiniEClient.data
{
    //public class sys_role_permission_rpc : ICloneable
    //{
    //    public string name { get; set; }
    //    public string desc { get; set; }

    //    public object Clone()
    //    {
    //        return this.MemberwiseClone();
    //    }
    //}
    public class SYSPermission : CheckableListViewItem
    {
        private sys_permission_rpc _data;
        public SYSPermission(sys_permission_rpc data)
        {
            _data = data;
        }

        public override string ID { get { return _data.name; } }
        public override string Name { get { return _data.name; } }
        public override string Desc { get { return _data.desc; } }
        public override object Value { get { return _data; } }

        public bool Equals(sys_permission_rpc rpc)
        {
            return _data.name == rpc.name;
        }
        public override bool Equals(object obj)
        {
            var rpc = obj as sys_permission_rpc;
            if (rpc != null)
                return Equals(rpc);
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return _data.name.GetHashCode();
        }
    }
    //public class sys_role_rpc : ICloneable
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //    public string desc { get; set; }
    //    public List<sys_role_permission_rpc> role_permissions { get; set; }
    //    public List<sys_group_rpc> role_groups { get; set; }

    //    public sys_role_rpc()
    //    {
    //        role_permissions = new List<sys_role_permission_rpc>();
    //        role_permissions.Add(new sys_role_permission_rpc() { name = "role", desc= "角色管理操作" });
    //        role_groups = new List<sys_group_rpc>();
    //    }

    //    public object Clone()
    //    {
    //        // TODO:
    //        return this.MemberwiseClone();
    //    }
    //}
    public class SYSRole : PNTreeViewItem
    {
        private sys_role_rpc _data;
        public override PNItemType Type
        {
            get
            {
                return PNItemType.LEAF | PNItemType.NOADD;
            }
        }


        public override string Id { get { return _data.name; } }
        public override string DisplayName { get { return _data.name; } }
        public override string Tips { get { return _data.desc; } }
        public override object Data { get { return _data; } }

        public override string Editor { get { return "SYSRoleEditor"; } }

        public SYSRole(sys_role_rpc data, PNTreeViewItem parent = null) : base(parent)
        {
            _data = data;
            /*SYSRole tmp;
            foreach (var element in data.children)
            {
                tmp = new SYSRole(element, this);
            }
            */
        }

        public override void UpdateData(object data)
        {
            var d = data as sys_role_rpc;
            _data.name = d.name;
            _data.desc = d.desc;
            _data.role_permissions = d.role_permissions;
        }

        public override object CloneData()
        {
            return _data.Clone();
        }
    }
}
