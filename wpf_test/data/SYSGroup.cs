using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class SYSGroup
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public bool is_system { get; set; }
        public string node { get; set; }
        public DateTime creation_time { get; set; }
    }
}
