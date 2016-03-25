using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Library
{
	public static class IEnumerableExtension
	{
		/// <summary>
		/// 自定义比较
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> data, Func<T, T, bool> comparer)
		{
			IEqualityComparer<T> comparerClass = new GeneralEqualityCompare<T>(comparer);
			return Enumerable.Distinct<T>(data, comparerClass);
		}

		/// <summary>
		/// 自定义比较
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				var elementValue = keySelector(element);
				if (seenKeys.Add(elementValue))
				{
					yield return element;
				}
			}
		}

		/// <summary>
		/// 自定义与非
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
		{
			IEqualityComparer<T> comparerClass = new GeneralEqualityCompare<T>(comparer);
			return Enumerable.Except<T>(first, second, comparerClass);
		}

		/// <summary>
		/// 自定义相交
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
		{
			IEqualityComparer<T> comparerClass = new GeneralEqualityCompare<T>(comparer);
			return Enumerable.Intersect<T>(first, second, comparerClass);
		}

		/// <summary>
		/// 自定义合并
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="comparer"></param>
		/// <returns></returns>
		public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
		{
			IEqualityComparer<T> comparerClass = new GeneralEqualityCompare<T>(comparer);
			return Enumerable.Union<T>(first, second, comparerClass);
		}

        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>true则为空, 否则为false</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.Any();
        }
	}
}
