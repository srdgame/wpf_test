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
        public string Id { get; set; }
        public bool IsExpand { get; set; }
        public string DisplayName { get; set; }
        public ObservableCollection<NodeItem> Children { get; set; }
        public NodeItem()
        {
            Id = Guid.NewGuid().ToString();
            Children = new ObservableCollection<NodeItem>();
        }
    }
}
