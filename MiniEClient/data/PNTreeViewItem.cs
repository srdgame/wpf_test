using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiniEClient.data
{
    public enum PNItemType
    {
        BOLE = 1,
        LEAF = 2,
        NOADD = 64,
        NOEDIT = 128,
        NODELETE = 256,
    }
    public class PNTreeViewItemList : ObservableCollection<IPNTreeViewItem> { }
    public interface IPNTreeViewItem
    {
        PNItemType Type { get; }
        string Id { get; }
        string DisplayName { get; }
        string Tips { get; }

        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsNew { get; set; }

        string Icon { get; }
        string AddIcon { get; }
        string DeleteIcon { get; }

        PNTreeViewItemList Children { get; }

        void UpdateData(object data);
        object CloneData();
        // For UI Binding Only
        object Data { get; }
    }

    public abstract class PNTreeViewItem : ViewModelBase, IPNTreeViewItem
    {
        private bool _is_expanded = false;
        private bool _is_selected = false;
        private bool _is_new = false;

        public bool IsExpanded { get { return _is_expanded; } set { _is_expanded = value; RaisePropertyChanged(() => IsExpanded); } }
        public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged(() => IsSelected); } }
        //public bool IsSelected { get { return _is_selected; } set { _is_selected = value; RaisePropertyChanged("IsSelected"); } }
        public bool IsNew { get { return _is_new; } set { _is_new = value; RaisePropertyChanged(() => IsNew); } }


        public string Icon { get { return Type.HasFlag(PNItemType.LEAF) ? "/images/icon/leaf.png" : "/images/icon/folder.png"; } }
        public string AddIcon { get { return Type.HasFlag(PNItemType.NOADD) ? "" : "/images/icon/add.png" ; } }
        //public string EditIcon { get { return Type.HasFlag(PropertyNodeType.NOEDIT) ? "" : "/images/icon/edit.png"; } }
        public string DeleteIcon { get { return Type.HasFlag(PNItemType.NODELETE) ? "" : "/images/icon/delete.png"; } }

        public PNTreeViewItemList Children { get; private set; }

        private PNTreeViewItem _parent;
        public PNTreeViewItem Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null)
                    {
                        _parent.Children.Remove(this);
                        _parent.UpdateGUI();
                    }
                    _parent = value;
                    if (_parent != null)
                    {
                        _parent.Children.Add(this);
                        _parent.UpdateGUI();
                    }
                }
            }
        }

        public abstract PNItemType Type { get; }
        public abstract string Id { get; }
        public abstract string DisplayName { get; }
        public abstract string Tips { get; }
        public abstract object Data { get; }

        public abstract void UpdateData(object data);
        public abstract object CloneData();
        public abstract string Editor { get; }

        public UserControl CreateEditor(object data_context)
        {
            var ename = this.Editor;
            if (ename == null || ename == string.Empty)
                return null;

            var type = System.Type.GetType("MiniEClient.ctrls." + ename);

            UserControl editor = Activator.CreateInstance(type) as UserControl;
            editor.DataContext = data_context;
            return editor;
        }

        public void UpdateGUI()
        {
            RaisePropertyChanged(() => Type);
            RaisePropertyChanged(() => DisplayName);
            RaisePropertyChanged(() => Tips);
            RaisePropertyChanged(() => Icon);
            RaisePropertyChanged(() => AddIcon);
            RaisePropertyChanged(() => DeleteIcon);
        }

        public PNTreeViewItem(PNTreeViewItem parent = null)
        {
            Parent = parent;
            Children = new PNTreeViewItemList();
        }
    }
}
