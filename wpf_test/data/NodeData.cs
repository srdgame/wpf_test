using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public enum NodeType
    {
        BOLE = 1,
        LEAF = 2,
        NODELETE = 4,
        NOEDIT = 8,
    }
    public interface IPNTreeItem : ICloneable
    {
        NodeType Type { get; }
        string Id { get; }
        string DisplayName { get; }
        string Tips { get; }
    }
    public class NodeDataList : ObservableCollection<NodeData> { }
    public abstract class NodeData : IPNTreeItem
    { 
        public NodeDataList Children { get; private set; }

        private NodeData _parent;
        public NodeData Parent
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

        public abstract NodeType Type { get; }
        public abstract string Id { get; }
        public abstract string DisplayName { get; }
        public abstract string Tips { get; }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public PropertyNodeItem Item { get; private set; }

        public NodeData(NodeData parent)
        {
            Parent = parent;
            Children = new NodeDataList();
            Item = new PropertyNodeItem()
            {
                Data = this,
            };
        }
    }
}
