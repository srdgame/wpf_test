using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class sys_user_rpc : ICloneable
    {
        public string id { get; set; }
        public string username { get; set; }
        public string cellphone { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public cm_node_rpc code { get; set; }
        public string email { get; set; }
        public sys_user_rpc creator { get; set; }
        public DateTime creation_time { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
