using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тест2
	/// </summary>
	[Serializable]
	[Flags]
	public enum Test2 : byte
	{
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData = 0,
		/// <summary>
		/// Тест2
		/// </summary>
		Test2 = 1,
		/// <summary>
		/// Тест2
		/// </summary>
		Test2 = 2,
		/// <summary>
		/// Тест2
		/// </summary>
		Test2 = 4,
	}
}