using System.Linq;

namespace RunForrest.Core.Util
{
    public static class ArrayExtensions
    {
        public static object[] NullIfEmpty(this object[] array)
        {
            return array == null || !array.Any() ? null : array;
        }

        //public static object[] EmptyIfNull(this object[] array)
        //{
        //    return array ?? new object[0];
        //}

        public static T[] EmptyIfNull<T>(this T[] array)
        {
            return array ?? new T[0];
        }
    }
}