using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using minie.irpc;

namespace MiniEClient.data
{
    public class SYSUser : PNTreeViewItem
    {
        private sys_user_rpc _data;
        public override PNItemType Type
        {
            get
            {
                return PNItemType.LEAF | PNItemType.NOADD;
            }
        }


        public override string Id { get { return _data.id; } }
        public override string DisplayName { get { return _data.fullname; } }
        public override string Tips { get { return _data.username; } }
        public override object Data { get { return _data; } }

        public override string Editor { get { return "SYSUserEditor"; } }

        public SYSUser(sys_user_rpc data, PNTreeViewItem parent = null) : base(parent)
        {
            _data = data;
        }

        public override void UpdateData(object data)
        {
            var d = data as sys_user_rpc;
            _data.username = d.username;
            _data.fullname = d.fullname;
            _data.email = d.email;
            _data.password = d.password;
            _data.cellphone = d.cellphone;
            _data.user_groups = d.user_groups;
        }

        public override object CloneData()
        {
            return _data.Clone();
        }
    }
}
