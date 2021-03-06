using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Обустройство
	/// </summary>
	[Serializable]
	[Flags]
	public enum Arrangements : byte
	{
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData = 0,
		/// <summary>
		/// Тележки смотровые
		/// </summary>
		InspectionTrolleys = 1,
		/// <summary>
		/// Люльки
		/// </summary>
		Cradles = 2,
		/// <summary>
		/// Смотровые хода
		/// </summary>
		ObservationMoves = 4,
		/// <summary>
		/// Люки
		/// </summary>
		Hatches = 8,
		/// <summary>
		/// Двери
		/// </summary>
		Doors = 16,
		/// <summary>
		/// Лестницы
		/// </summary>
		Stairs = 32,
	}
}