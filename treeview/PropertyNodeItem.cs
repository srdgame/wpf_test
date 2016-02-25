using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treeview
{
    public class PropertyNodeData
    {
        public string ADD_ICON = "images/icon/add.png";
        public string EDITABLE_ICON = "images/icon/edit.png";
        public string DELETE_ICON = "images/icon/delete.png";
        public string FOLDER_ICON = "images/icon/folder.png";
        public string TAG_ICON = "images/icon/tag.png";

        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public ObservableCollection<PropertyNodeData> Children { get; set; }

        public PropertyNodeItem Item
        {
            get
            {
                return new PropertyNodeItem()
                {
                    Data = this,
                    Icon = FOLDER_ICON,
                    AddIcon = ADD_ICON,
                    EditIcon = EDITABLE_ICON,
                    DeleteIcon = DELETE_ICON,
                };
            }
        }
        public PropertyNodeData()
        {
            Children = new ObservableCollection<PropertyNodeData>();
        }
    }
    public class PropertyNodeItem : ViewModelBase, ICloneable
    {
        public string Icon { get; set; }
        public string AddIcon { get; set; }
        public string EditIcon { get; set; }
        public string DeleteIcon { get; set; }

        public PropertyNodeData Data = new PropertyNodeData();
        public string Id
        {
            get { return Data.id; }
            set
            {
                if (Data.id != value)
                {
                    Data.id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }
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

        public PropertyNodeItem()
        {
        }

        public object Clone()
        {
            return new PropertyNodeItem()
            {
                Icon = this.Icon,
                AddIcon = this.AddIcon,
                EditIcon = this.EditIcon,
                DeleteIcon = this.DeleteIcon,
                DisplayName = this.DisplayName,
                Tips = this.Tips,
            };
        }
    }
}
