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
        public override object Data
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string DisplayName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type Editor
        {
            get
            {
                return typeof(ctrls.SYSGroupEditor);
            }
        }

        public override string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Tips
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override PNItemType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object CloneData()
        {
            throw new NotImplementedException();
        }

        public override void UpdateData(object data)
        {
            throw new NotImplementedException();
        }
    }
}
