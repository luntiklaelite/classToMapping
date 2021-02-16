using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тест1
	/// </summary>
	[Serializable]
	[Flags]
	public enum Test1 : byte
	{
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData = 0,
		/// <summary>
		/// Тест1
		/// </summary>
		Test1 = 1,
		/// <summary>
		/// Тест1
		/// </summary>
		Test1 = 2,
		/// <summary>
		/// Тест1
		/// </summary>
		Test1 = 4,
		/// <summary>
		/// Тест1
		/// </summary>
		Test1 = 8,
		/// <summary>
		/// Тест1
		/// </summary>
		Test1 = 16,
	}
}