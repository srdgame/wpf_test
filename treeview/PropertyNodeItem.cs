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
        public string DisplayName { get; set; }
        public string Tips { get; set; }
    }
    public class PropertyNodeItem : ViewModelBase, ICloneable
    {
        public string Icon { get; set; }
        public string AddIcon { get; set; }
        public string EditIcon { get; set; }
        public string DeleteIcon { get; set; }

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
        public PropertyNodeItem()
        {
            Data = new PropertyNodeData();
            Children = new ObservableCollection<PropertyNodeItem>();
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
