using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип покрытия проезжей части
	/// </summary>
	[Serializable]
	public enum CoverType : byte
	{
		/// <summary>
		/// Асфальтобетон
		/// </summary>
		AsphaltConcrete,
		/// <summary>
		/// Цементобетон
		/// </summary>
		CementConcrete,
		/// <summary>
		/// Черный щебень
		/// </summary>
		BlackRubble,
		/// <summary>
		/// Каменная мостовая
		/// </summary>
		StonePavement,
		/// <summary>
		/// Дощатый настил
		/// </summary>
		Boardwalk,
		/// <summary>
		/// Песчано-гравийная смесь
		/// </summary>
		SandAndGravel,
		/// <summary>
		/// Грунтовое
		/// </summary>
		Ground,
		/// <summary>
		/// Ж.б. плиты по цементно-песчаной подготовке
		/// </summary>
		FerroconcreteSlabsCementSand,
	}
}