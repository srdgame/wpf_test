using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_test.data
{
    public interface Wrapper<T>
    {
        T Value { get; }
        bool Equals(T obj); 
    }
    public abstract class CheckableListViewItem : ViewModelBase
    {
        public bool IsSelected { get; set; }
        public abstract string ID { get; }
        public abstract string Name { get; }
        public abstract string Desc { get; }
        public abstract object Value { get; }

        public CheckableListViewItem()
        {
        }
    }
}
