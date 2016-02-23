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
    }
    public class PropertyNodeItem : ViewModelBase, ICloneable
    {
        public string Id { get; set; }
        public PropertyNodeType Type { get; }
        public string Icon { get { return Type.HasFlag(PropertyNodeType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png";} }
        public string AddIcon { get { return Type.HasFlag(PropertyNodeType.BOLE) ? "/images/icon/add.png" : ""; } }
        //public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(PropertyNodeType.NODELETE) ? "" : "/images/icon/delete.png"; } }
        private bool _is_expanded;
        private bool _is_selected;
        public bool IsExpanded { get { return _is_expanded; } set { _is_expanded = value; RaisePropertyChanged(() => IsExpanded); } }
        //public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged(() => IsSelected); } }
        public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged("IsSelected"); } }
        private string _display_name;
        public string DisplayName
        {
            get { return _display_name; }
            set
            {
                if (_display_name != value)
                {
                    _display_name = value;
                    RaisePropertyChanged(() => DisplayName);
                    //RaisePropertyChanged("DisplayName");
                }
            }
        }
        private string _tips;
        public string Tips
        {
            get { return _tips; }
            set
            {
                if (_tips != value)
                {
                    _tips = value;
                    RaisePropertyChanged(() => Tips);
                }
            }
        }

        public ObservableCollection<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem(PropertyNodeType type)
        {
            Type = type;
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
