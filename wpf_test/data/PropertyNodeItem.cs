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
        public string Icon {
            get {
                if (Type.HasFlag(PropertyNodeType.LEAF))
                {
                    return "/images/icon/leaf.png";
                }
                return "/images/icon/folder.png";
            }
        }
        public string AddIcon
        {
            get
            {
                if (Type.HasFlag(PropertyNodeType.BOLE))
                {
                    return "/images/icon/add.png";
                }
                return "";
            }
        }
        public string EditIcon
        {
            get
            {
                if (Type.HasFlag(PropertyNodeType.NOEDIT))
                {
                    return "";
                }
                return "/images/icon/edit.png";
            }
        }
        public string DeleteIcon
        {
            get
            {
                if (Type.HasFlag(PropertyNodeType.NODELETE))
                {
                    return "";
                }
                return "/images/icon/delete.png";
            }
        }

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
