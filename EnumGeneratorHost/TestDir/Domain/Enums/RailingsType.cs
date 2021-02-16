using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип перил
	/// </summary>
	[Serializable]
	public enum RailingsType : byte
	{
		/// <summary>
		/// Перила отсутствуют
		/// </summary>
		None,
		/// <summary>
		/// Металлические (секционные или непрерывные)
		/// </summary>
		Metallic,
		/// <summary>
		/// Железобетонный поручень с металлической решеткой
		/// </summary>
		FerroncreteMetalGrating,
		/// <summary>
		/// Железобетонные (решетчатые или со сплошной стенкой)
		/// </summary>
		FerroconcreteLlatticeOrSolidWall,
		/// <summary>
		/// Деревянные
		/// </summary>
		Wood,
		/// <summary>
		/// Комбинированные
		/// </summary>
		Combined,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}