using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace Package.SecondOrder.Editor
{
    /// <summary>
    /// Helper class for custom property drawers.
    /// </summary>
    public static class DrawerHelper
    {
        /// <summary>
        /// Get the target object of a property.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static object GetTargetObjectOfProperty(SerializedProperty prop)
        {
            if (prop == null) return null;

            string path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            string[] elements = path.Split('.');

            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string name = element.Substring(0, element.IndexOf("[", StringComparison.Ordinal));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[", StringComparison.Ordinal))
                        .Replace("[", "").Replace("]", ""));
                    obj = GetValue(obj, name, index);
                }
                else
                {
                    obj = GetValue(obj, element);
                }

                if (obj == null) return null;
            }

            return obj;
        }

        /// <summary>
        /// Get the value of a field or property by name.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static object GetValue(object source, string name)
        {
            if (source == null) return null;
            Type type = source.GetType();
            FieldInfo field = null;
            while (type != null)
            {
                field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null) break;
                type = type.BaseType;
            }

            return field?.GetValue(source);
        }

        /// <summary>
        /// Get the value of a field or property by name and index.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static object GetValue(object source, string name, int index)
        {
            if (GetValue(source, name) is not IEnumerable enumerable)
                return null;

            IEnumerator enumerator = enumerable.GetEnumerator();
            IDisposable disposable = enumerator as IDisposable;

            try
            {
                for (int i = 0; i <= index; i++)
                {
                    if (!enumerator.MoveNext())
                        return null;
                }

                return enumerator.Current;
            }
            finally
            {
                disposable?.Dispose();
            }
        }
    }
}