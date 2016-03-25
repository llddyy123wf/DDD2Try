using System;
using System.Collections.Generic;

namespace Framework.Library
{
	/// <summary>
	/// 通用对象比较器
	/// 用户集合的Disctinct等操作
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GeneralEqualityCompare<T> : IEqualityComparer<T>
	{
		private Func<T, T, bool> comparer = null;

		#region Constructor
		public GeneralEqualityCompare(Func<T, T, bool> comparer)
		{
			this.comparer = comparer;
		}
		#endregion

		/// <summary>
		/// 判断x,y是否相等
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Equals(T x, T y)
		{
			bool result = false;
			if (this.comparer != null)
			{
				result = this.comparer(x, y);
			}
			else
			{
				result = object.Equals(x, y);
			}

			return result;
		}

		public int GetHashCode(T obj)
		{
			return base.GetHashCode();
		}
	}

	/// <summary>
	/// 通用对象比较, 适用Sort
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GeneralCompare<T> : IComparer<T>
	{
		private Func<T, T, bool> comparer = null;

		#region Constructor
		public GeneralCompare(Func<T, T, bool> comparer)
		{
			this.comparer = comparer;
		}
		#endregion

		public int Compare(T x, T y)
		{
			int result = 0;
			if (this.comparer != null)
			{
				result = this.comparer(x, y) ? 1 : -1;
			}
			return result;
		}
	}
}
