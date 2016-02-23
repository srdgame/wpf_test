using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comboxtreeview
{
    class NodeItem
    {
        public string DisplayName { get; set; }
        public ObservableCollection<NodeItem> Children { get; set; }
        public NodeItem()
        {
            Children = new ObservableCollection<NodeItem>();
        }
    }
}
