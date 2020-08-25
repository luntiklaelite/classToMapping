//Generated by EnumGenerator, 25.08.2020 10:59:08
using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип тротуара
	/// </summary>
	[Serializable]
	public enum SidewalkType
	{
		/// <summary>
		/// Тротуаров нет (зоны для прохода пешеходов)
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
