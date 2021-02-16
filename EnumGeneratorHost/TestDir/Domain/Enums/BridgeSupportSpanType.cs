using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Опорная часть
	/// </summary>
	[Serializable]
	public enum BridgeSupportSpanType : byte
	{
		/// <summary>
		/// Эластомерная
		/// </summary>
		Elastomeric,
		/// <summary>
		/// Металлическая
		/// </summary>
		Metal,
		/// <summary>
		/// Комбинированная
		/// </summary>
		Combined,
		/// <summary>
		/// Прочее
		/// </summary>
		Other,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}