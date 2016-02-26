using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class NodeDataList : ObservableCollection<PNDataBase> { }
    public abstract class PNDataBase : IPNTreeViewItem
    { 
        public NodeDataList Children { get; private set; }

        private PNDataBase _parent;
        public PNDataBase Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null)
                        _parent.Children.Remove(this);
                    _parent = value;
                    if (_parent != null)
                        _parent.Children.Add(this);
                }
            }
        }

        public abstract PNItemType Type { get; }
        public abstract string Id { get; }
        public abstract string DisplayName { get; }
        public abstract string Tips { get; }
        public abstract void UpdateData(object data);
        public abstract object CloneData();

        public PNTreeViewItem Item { get; private set; }

        public PNDataBase(PNDataBase parent)
        {
            Parent = parent;
            Children = new NodeDataList();
            Item = new PNTreeViewItem(this);
        }
    }
}
