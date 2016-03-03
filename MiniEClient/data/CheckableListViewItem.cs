using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniEClient.data
{
    public abstract class CheckableListViewItem : ViewModelBase
    {
        private bool _is_selected = false;
        public bool IsSelected
        {
            get { return _is_selected; }
            set { _is_selected = value; RaisePropertyChanged(() => IsSelected); }
        }
        public abstract string ID { get; }
        public abstract string Name { get; }
        public abstract string Desc { get; }
        public abstract object Value { get; }

        public CheckableListViewItem()
        {
        }
    }
}
