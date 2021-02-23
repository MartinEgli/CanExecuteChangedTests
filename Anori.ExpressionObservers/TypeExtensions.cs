using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Anori.ExpressionObservers
{
    public static class TypeExtensions
    {
        public static bool IsNullable([NotNull] this TypeInfo genericTypeInfo)
        {
            if (genericTypeInfo == null)
            {
                throw new ArgumentNullException(nameof(genericTypeInfo));
            }

            return Nullable.GetUnderlyingType(genericTypeInfo) != null;
        }

        public static bool IsNullable([NotNull] this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsNullable<T>()
        {
            return typeof(T).IsNullable();
        }
    }

    public static class EnumerableExtensions
    {
        public static TSource? ValueAtOrNull<TSource>(this IEnumerable<TSource> source, int index)
            where TSource : struct =>
            source.ElementAtOrNull(index);

        public static TSource ReferenceAtOrNull<TSource>(this IEnumerable<TSource> source, int index)
            where TSource : class =>
            source.ElementAtOrNull(index);

        public static TSource? ValueAtOrNull<TSource>(this IList<TSource> source, int index)
            where TSource : struct =>
            source.ElementAtOrNull(index);

        public static TSource ReferenceAtOrNull<TSource>(this IList<TSource> source, int index)
            where TSource : class =>
            source.ElementAtOrNull(index);
    }

    public static class ValueTypeExtensions
    {
        public static TSource? ElementAtOrNull<TSource>(this IEnumerable<TSource> source, int index) where TSource : struct
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (index >= 0)
            {
                if (source is IList<TSource> list)
                {
                    if (index < list.Count) return list[index];
                }
                else
                {
                    using var e = source.GetEnumerator();
                    while (true)
                    {
                        if (!e.MoveNext())
                        {
                            break;
                        }

                        if (index == 0)
                        {
                            return e.Current;
                        }
                        index--;
                    }
                }
            }
            return null;
        }

        public static TSource? ElementAtOrNull<TSource>(this IList<TSource> source, int index) where TSource : struct
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (index >= 0)
            {
                if (index < source.Count)
                {
                    return source[index];
                }
            }
            return null;
        }

        public static TValue? GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : struct
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }
    }

    public static class ReferenceTypeExtensions
    {
        public static TSource ElementAtOrNull<TSource>(this IEnumerable<TSource> source, int index) where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (index >= 0)
            {
                if (source is IList<TSource> list)
                {
                    if (index < list.Count) return list[index];
                }
                else
                {
                    using var e = source.GetEnumerator();
                    while (true)
                    {
                        if (!e.MoveNext())
                        {
                            break;
                        }

                        if (index == 0)
                        {
                            return e.Current;
                        }
                        index--;
                    }
                }
            }
            return null;
        }

        public static TSource ElementAtOrNull<TSource>(this IList<TSource> source, int index) where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (index >= 0)
            {
                if (index < source.Count)
                {
                    return source[index];
                }
            }
            return null;
        }

        public static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }
    }
}