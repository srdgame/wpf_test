using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public enum PNItemType
    {
        BOLE = 1,
        LEAF = 2,
        NODELETE = 4,
        NOEDIT = 8,
    }
    public interface IPNTreeViewItem : ICloneable
    {
        PNItemType Type { get; }
        string Id { get; }
        string DisplayName { get; }
        string Tips { get; }
    }
    public class PNTreeViewItem : ViewModelBase
    {
        private bool _is_expanded = false;
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
        public string Icon { get { return Type.HasFlag(PNItemType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png"; } }
        public string AddIcon { get { return Type.HasFlag(PNItemType.BOLE) ? "/images/icon/add.png" : ""; } }
        //public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(PNItemType.NODELETE) ? "" : "/images/icon/delete.png"; } }

        public IPNTreeViewItem Owner { get; private set; }
        public PNItemType Type { get { return Owner.Type; } }
        public string DisplayName {  get { return Owner.DisplayName; } }
        public string Tips { get { return Owner.Tips; } }

        public PNTreeViewItem(IPNTreeViewItem owner)
        {
            Owner = owner;
        }
    }
}
