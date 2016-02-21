using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class CMNodeBase
    {
        public string id{ get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string address { get; set; }
        public int category { get; set; }
        public string parent { get; set; }
        public string creator { get; set; }
        public DateTime creation_time { get; set; }
    }
}
