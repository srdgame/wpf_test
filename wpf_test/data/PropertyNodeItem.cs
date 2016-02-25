using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public class PropertyNodeItem : ViewModelBase
    {
        private bool _is_expanded = true;
        private bool _is_selected = false;
        private bool _is_new = false;

        public bool IsExpanded { get { return _is_expanded; } set { _is_expanded = value; RaisePropertyChanged(() => IsExpanded); } }
        public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged(() => IsSelected); } }
        //public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged("IsSelected"); } }
        public bool IsNew { get { return _is_new; } set { _is_new = value; RaisePropertyChanged(() => IsNew); } }
        
        public void UpdateGUI()
        {
            RaisePropertyChanged(() => Type);
            RaisePropertyChanged(() => DisplayName);
            RaisePropertyChanged(() => Tips);
            RaisePropertyChanged(() => Icon);
            RaisePropertyChanged(() => AddIcon);
            RaisePropertyChanged(() => DeleteIcon);
        }
        public string Icon { get { return Type.HasFlag(NodeType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png"; } }
        public string AddIcon { get { return Type.HasFlag(NodeType.BOLE) ? "/images/icon/add.png" : ""; } }
        //public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(NodeType.NODELETE) ? "" : "/images/icon/delete.png"; } }

        private IPNTreeItem _data;
        public IPNTreeItem Data
        {
            get { return _data; }
            set
            {
                _data = value;
                UpdateGUI();
            }
        }
        public NodeType Type { get { return Data.Type; } }
        public string DisplayName {  get { return Data.DisplayName; } }
        public string Tips { get { return Data.Tips; } }

    }
}
