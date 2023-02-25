using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DapperObjector
{
    public static class ProperyHelper
    {
        public static string[] ExposePropertyNames(this object obj, bool doRemoveKey = false)
        {
            var props = obj.GetType().GetProperties();
            if (doRemoveKey)
                props = props.RemoveKey();

            var result = props.Select(prop => prop.Name).ToArray();
            return result;
        }

        public static string ExposeObjectName(this object obj)
        {
            try
            {
                var attr = (TableAttribute)obj.GetType().GetCustomAttribute(typeof(TableAttribute));
                return attr.Name;
            }
            catch
            {
                return obj.GetType().Name;
            }

        }

        public static KeyValuePair<string, object> GetObjectKey(this object obj)
        {
            var result = new KeyValuePair<string, long>();
            var keyObj = obj.GetType().GetProperties().Where(x => x.IsDefined(typeof(KeyAttribute), false)).FirstOrDefault();
            if (keyObj != null)
            {
                var key = keyObj.Name;
                var value = keyObj.GetValue(obj, null);
                return new KeyValuePair<string, object>(key, value);
            }

            return default(KeyValuePair<string, object>);


        }

        private static PropertyInfo[] RemoveKey(this PropertyInfo[] props)
        {
            for (int i = 0; i < props.Length; i++)
            {
                if (Attribute.IsDefined(props[i], typeof(KeyAttribute)))
                {
                    props = props.RemoveAt(i);
                }

            }

            return props;
        }

        private static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }


    }




}
