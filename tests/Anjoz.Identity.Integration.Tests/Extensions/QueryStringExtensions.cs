using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Anjoz.Identity.Integration.Tests.Extensions
{
    public  static class QueryStringExtensions
    {
        public static string ToQueryString(this object obj)
        {
            if (!obj.GetType().IsComplex())
            {
                return obj.ToString();
            }

            var values = obj
                .GetType()
                .GetProperties()
                .Where(o => o.GetValue(obj, null) != null);

            var result = new List<string>();

            foreach (var value in values)
            {
                // Avaliar quando precisar de adicionar Lista
                // if (!typeof(string).IsAssignableFrom(value.PropertyType) 
                //     && typeof(IEnumerable).IsAssignableFrom(value.PropertyType))
                // {
                //     var items = value.GetValue(obj) as IList;
                //     if (items.Count > 0)
                //     {
                //         for (int i = 0; i < items.Count; i++)
                //         {
                //             result = result.Add(value.Name, ToQueryString(items[i]));
                //         }
                //     }
                // }
                if (value.PropertyType.IsComplex())
                {
                    result.Add($"{ToQueryString(value.GetValue(obj), value.Name)}");
                }
                else
                {
                   result.Add($"{value.Name}={HttpUtility.UrlEncode(value.GetValue(obj, null).ToString())}");
                }
            }

            return String.Join("&", result.ToArray());
        }
        
        private static string ToQueryString(object obj, string name)
        {
            if (!obj.GetType().IsComplex())
            {
                return obj.ToString();
            }

            var values = obj
                .GetType()
                .GetProperties()
                .Where(o => o.GetValue(obj, null) != null);

            var result = new List<string>();

            foreach (var value in values)
            {
                // Avaliar quando precisar de adicionar Lista
                // if (!typeof(string).IsAssignableFrom(value.PropertyType) 
                //     && typeof(IEnumerable).IsAssignableFrom(value.PropertyType))
                // {
                //     var items = value.GetValue(obj) as IList;
                //     if (items.Count > 0)
                //     {
                //         for (int i = 0; i < items.Count; i++)
                //         {
                //             result = result.Add(value.Name, ToQueryString(items[i]));
                //         }
                //     }
                // }
                if (value.PropertyType.IsComplex())
                {
                    result.Add(ToQueryString(value.GetValue(obj), $"{name}.{value.Name}"));
                }
                else
                {
                    result.Add($"{name}.{value.Name}={HttpUtility.UrlEncode(value.GetValue(obj, null).ToString())}");
                }
            }

            return String.Join("&", result.ToArray());
        }

        private static bool IsComplex(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsComplex(typeInfo.GetGenericArguments()[0]);
            }
            return !(typeInfo.IsPrimitive
                     || typeInfo.IsEnum
                     || type.Equals(typeof(Guid))
                     || type.Equals(typeof(string))
                     || type.Equals(typeof(decimal)));
        }
    }
}