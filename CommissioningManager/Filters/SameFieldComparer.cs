using CommissioningManager.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommissioningManager.Filters
{
    public class SameFieldComparer<T> : IEqualityComparer<T> where T : class
    {
        public SameFieldComparer() { }

        public bool Equals(T x, T y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            List<bool> foundlist = new List<bool>();

            Type objtypeX = x.GetType();
            Type objtypeY = y.GetType();

            var PropertyInfoList = objtypeX.GetProperties().Join(objtypeY.GetProperties(), a => a.Name, b => b.Name, (a, b) => new { objX = a, objY = b })
                .Where(z => z.objX.GetCustomAttributes(typeof(Conditions), false).Count() > 0);

            foreach (var item in PropertyInfoList)
            {
                var aX = item.objX.GetCustomAttributes(typeof(Conditions), false);
                var aY = item.objY.GetCustomAttributes(typeof(Conditions), false);

                if ((aX.First() as Conditions).CompareRequiredSameField && (aY.First() as Conditions).CompareRequiredSameField)
                {
                    var t1 = x.GetType().GetProperty(item.objX.Name).GetValue(x);
                    var t2 = y.GetType().GetProperty(item.objY.Name).GetValue(y);

                    if (t1 != null && t2 != null)
                    {
                        foundlist.Add(t1.ToString().ToLower() == t2.ToString().ToLower());
                    }
                }
            }
            return foundlist.All(q => q);
        }

        public int GetHashCode(T obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;

            List<int> intList = new List<int>();

            Type objtypeX = obj.GetType();

            var PropertyInfoList = objtypeX.GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);

            foreach (var item in PropertyInfoList)
            {
                var aX = item.GetCustomAttributes(typeof(Conditions), false);

                if ((aX.First() as Conditions).CompareRequiredSameField)
                {
                    var t1 = obj.GetType().GetProperty(item.Name).GetValue(obj);

                    if (t1 != null)
                    {
                        intList.Add(t1.GetHashCode());
                    }
                }
            }
            if (intList.Count() == 0)
                intList.Add(1);
            return intList.Aggregate((x, j) => x ^ j);
        }
    }
}
