using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{ 
    public enum PropertyNodeType
    {
        BOLE = 1,
        LEAF = 2,
        NODELETE = 4,
        NOEDIT = 8,
        NOLOCK = 256,
    }
    public class PropertyNodeItemBase : ViewModelBase
    {
        public PropertyNodeType Type { get; set; }
        private bool _is_expanded = false;
        private bool _is_selected = false;
        public bool IsExpanded { get { return _is_expanded; } set { _is_expanded = value; RaisePropertyChanged(() => IsExpanded); } }
        //public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged(() => IsSelected); } }
        public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged("IsSelected"); } }
        public ObservableCollection<PropertyNodeItemBase> Children { get; set; }

        public PropertyNodeItemBase(PropertyNodeType type)
        {
            Type = type;
            Children = new ObservableCollection<PropertyNodeItemBase>();
        }
    }
    public class PropertyNodeItem <T> : PropertyNodeItemBase where T : NodeData
    {
        private T _data;
        public T Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged(() => DisplayName);
                RaisePropertyChanged(() => Tips);
            }
        }
        public string Id { get { return Data.id; } }
        public string Icon { get { return Type.HasFlag(PropertyNodeType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png";} }
        public string AddIcon { get { return Type.HasFlag(PropertyNodeType.BOLE) ? "/images/icon/add.png" : ""; } }
        //public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(PropertyNodeType.NODELETE) ? "" : "/images/icon/delete.png"; } }

        public string DisplayName
        {
            get { return Data.name; }
            set
            {
                if (Data.name != value)
                {
                    Data.name = value;
                    RaisePropertyChanged(() => DisplayName);
                    //RaisePropertyChanged("DisplayName");
                }
            }
        }
        public string Tips
        {
            get { return Data.desc; }
            set
            {
                if (Data.desc != value)
                {
                    Data.desc = value;
                    RaisePropertyChanged(() => Tips);
                }
            }
        }

        public PropertyNodeItem(PropertyNodeType type, T data) : base(type)
        {
            Data = data;
        }
    }
}
