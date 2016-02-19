using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{ 
    public class PropertyNodeData
    {
        public string DisplayName { get; set; }
        public string Tips { get; set; }
    }
    public enum PropertyNodeType
    {
        BOLE = 1,
        LEAF = 2,
        NODELETE = 4,
        NOEDIT = 8,
    }
    public class PropertyNodeItem : ViewModelBase, ICloneable
    {
        public PropertyNodeType Type { get; }
        public string Icon { get { return Type.HasFlag(PropertyNodeType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png";} }
        public string AddIcon { get { return Type.HasFlag(PropertyNodeType.BOLE) ? "/images/icon/add.png" : ""; } }
        public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(PropertyNodeType.NODELETE) ? "" : "/images/icon/delete.png"; } }
        private bool _is_expanded;
        private bool _is_selected;
        public bool IsExpanded { get { return _is_expanded; } set { _is_expanded = value; RaisePropertyChanged(() => IsExpanded); } }
        //public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged(() => IsSelected); } }
        public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged("IsSelected"); } }
        private PropertyNodeData Data;
        public string DisplayName
        {
            get { return Data.DisplayName; }
            set
            {
                if (Data.DisplayName != value)
                {
                    Data.DisplayName = value;
                    RaisePropertyChanged(() => DisplayName);
                    //RaisePropertyChanged("DisplayName");
                }
            }
        }
        public string Tips
        {
            get { return Data.Tips; }
            set
            {
                if (Data.Tips != value)
                {
                    Data.Tips = value;
                    RaisePropertyChanged(() => Tips);
                }
            }
        }

        public ObservableCollection<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem(PropertyNodeType type)
        {
            Type = type;
            Data = new PropertyNodeData();
            Children = new ObservableCollection<PropertyNodeItem>();
        }

        public object Clone()
        {
            return new PropertyNodeItem(this.Type)
            {
                DisplayName = this.DisplayName,
                Tips = this.Tips,
            };
        }
    }
}
