using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class CMFriendNode
    {
        public string id { get; set; }
    }
    public class CMUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string cellphone { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public DateTime creation_time { get; set; }
        public List<CMFriendNode> friends;
    }
}
