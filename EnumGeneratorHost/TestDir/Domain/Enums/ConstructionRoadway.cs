using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Конструкция проезжей части
	/// </summary>
	[Serializable]
	public enum ConstructionRoadway : byte
	{
		/// <summary>
		/// Железобетонная плита в составе основной несущей железобетонной конструкции
		/// </summary>
		FerroconcreteSlabInMainSupStruct,
		/// <summary>
		/// Железобетонная плита, включенная в совместную работу с металлическими главными балками (в сталежелезобетонных пролетных строениях)
		/// </summary>
		FerroconcreteSlabWithPiles,
		/// <summary>
		/// Железобетонная плита по балкам без объединения
		/// </summary>
		FerroconcreteSlabOnPiles,
		/// <summary>
		/// Ортотропная плита в составе главных и поперечных балок
		/// </summary>
		OrthotropicSlab,
		/// <summary>
		/// Деревянная
		/// </summary>
		Wood,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}