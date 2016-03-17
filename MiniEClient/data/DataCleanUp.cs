using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minie.irpc
{
    public static class DataCleanUp
    {

        private static Ice.Object IceCleanupEx(this Ice.Object obj)
        {
            var v = Activator.CreateInstance(obj.GetType()) as Ice.Object;
            var type = obj.GetType();
            var id = type.GetProperty("id");
            if (id != null)
            {
                id.SetValue(v, id.GetValue(obj));
            }
            else
            {
                var name = type.GetProperty("name");
                name.SetValue(v, name.GetValue(obj));
            }
            return v;
        }

        public static T CleanupEx<T>(this T obj, bool listEmpty = false) where T : ICloneable
        {
            var new_obj = obj.Clone();
            var type = obj.GetType();
            var plist = type.GetProperties();
            foreach( var pi in plist)
            {
                var p = pi.GetValue(obj);
                if (p as IList != null)
                {
                    if (listEmpty)
                        pi.SetValue(obj, null);
                    else
                        pi.SetValue(obj, (p as IList).CleanupEx());
                }
                if (p as Ice.Object != null)
                {
                    pi.SetValue(obj, (p as Ice.Object).IceCleanupEx());
                }
            }
            return (T)new_obj;
        }
        public static IList CleanupEx(this IList list)
        {
            var r = Activator.CreateInstance(list.GetType()) as IList;
            foreach ( var item in list)
            {
                if (item as ICloneable != null)
                    r.Add(DataCleanUp.CleanupEx(item as ICloneable, true));
            }
            return r;
        }
    }
}
