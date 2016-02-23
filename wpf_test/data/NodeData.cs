using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public interface NodeData : ICloneable
    {
        string id { get; set; }
        string name { get; set; }
        string desc { get; set; }
    }
}
