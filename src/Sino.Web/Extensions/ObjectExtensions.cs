using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// 类型强制转换
		/// </summary>
		public static T As<T>(this object obj) where T : class
		{
			return (T)obj;
		}

		/// <summary>
		/// 利用<see cref="Convert.ChangeType(object,System.TypeCode)"/>方法转换类型
		/// </summary>
		public static T To<T>(this object obj)
			where T : struct
		{
			return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// 判断该对象是否在指定的数组中
		/// </summary>
		public static bool IsIn<T>(this T item, params T[] list)
		{
			return list.Contains(item);
		}

		/// <summary>
		/// 判断是否不为Null
		/// </summary>
		public static bool IsNotNull(this object obj)
		{
			if (obj == null)
				return false;

			if (obj is string)
			{
				return !string.IsNullOrEmpty(obj as string);
			}
			return true;
		}

		/// <summary>
		/// 判断是否不为NULL，如果不为则调用Action
		/// </summary>
		public static void IsNotNull(this object obj, Action action)
		{
			if (obj.IsNotNull())
				action?.Invoke();
		}

		/// <summary>
		/// string转Guid
		/// </summary>
		public static Guid? StringToGuid(string s)
		{
			Guid guid = Guid.NewGuid();
			if (Guid.TryParse(s, out guid))
			{
				return guid;
			}
			else
			{
				return null;
			}
		}
	}
}