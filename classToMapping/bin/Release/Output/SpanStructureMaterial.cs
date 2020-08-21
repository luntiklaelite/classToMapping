using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Материал пролётного строения
	/// </summary>
	[Serializable]
	public enum SpanStructureMaterial
	{
		/// <summary>
		/// Железобетон преднапряженный
		/// </summary>
		ReinforcedConcretePrestressed,
		/// <summary>
		/// Железобетон
		/// </summary>
		Ferroconcrete,
		/// <summary>
		/// Сталь
		/// </summary>
		Steel,
		/// <summary>
		/// Сталежелезобетон
		/// </summary>
		SteelКeinforcedСoncrete,
		/// <summary>
		/// Древесина
		/// </summary>
		Wood,
		/// <summary>
		/// Древесина клееная
		/// </summary>
		GluedTimber,
		/// <summary>
		/// Каменная или бетонная кладка
		/// </summary>
		StoneOrConcreteMasonry,
		/// <summary>
		/// Алюминий
		/// </summary>
		Aluminum,
		/// <summary>
		/// Композитный материал
		/// </summary>
		CompositeMaterial,
	}
}
