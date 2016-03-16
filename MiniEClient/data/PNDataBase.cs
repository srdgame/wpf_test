using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiniEClient.data
{
    //public static class ToStringExClass
    //{
    //    public static string ToStringEx(this cm_entrance_rpc obj)
    //    {
    //        return string.Format("{0} Ex!!!", obj.ToString());
    //    }
    //    public static string ToStringEx(this object obj)
    //    {
    //        return string.Format("{0} Ex!!!", obj.ToString());
    //    }
    //    public static void test()
    //    {
    //        cm_entrance_rpc rpc = new cm_entrance_rpc();
    //        rpc.ToStringEx();
    //    }
    //}

    public class EqualsChecker
    {
        private static Dictionary<string, Func<object, object, bool>> Expressions = new Dictionary<string, Func<object, object, bool>>();
        public static bool Check(object obj1, object obj2)
        {
            var Type = obj1.GetType();
            if (!Expressions.ContainsKey(Type.FullName))
                return obj1.Equals(obj2);
            return Expressions[Type.FullName](obj1, obj2);
        }
        public static void Reg<T>(Func<object, object, bool> equalExpression)
        {
            Expressions.Add(typeof(T).FullName, equalExpression);
        }
    }
    public abstract class EqualsRegister<T, ET>
    {
        protected abstract bool Equals(T obj1, object obj2);

        public static EqualsRegister<T, ET> instance = GetInstance();
        static EqualsRegister<T, ET> GetInstance()
        {
            Type t = typeof(ET);
            return Activator.CreateInstance(t) as EqualsRegister<T, ET>;
        }
        protected EqualsRegister()
        {
            EqualsChecker.Reg<T>(this.GetExp());
        }
        Func<object, object, bool> GetExp()
        {
            return EqualsFunc;
        }
        bool EqualsFunc(object obj1, object obj2)
        {
            T t1 = (T)obj1;
            if (t1 != null)
            {
                if (Equals(t1, obj2))
                    return true;
            }
            T t2 = (T)obj2;
            if (t2 != null)
            {
                if (Equals(t2, obj1))
                    return true;
            }

            return obj1.Equals(obj2);
        }

    }
    public class Wrapper<T>
    {
        private T Value { get; set; }
        public Wrapper(T value)
        {
            Value = value;
        }
        static public implicit operator T(Wrapper<T> obj)
        {
            return obj.Value;
        }
    }
}
