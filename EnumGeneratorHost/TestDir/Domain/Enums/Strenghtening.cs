using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Укрепление
	/// </summary>
	[Serializable]
	public enum Strenghtening : byte
	{
		/// <summary>
		/// Одерновка
		/// </summary>
		Odernovka,
		/// <summary>
		/// Каменная наброска, мощение
		/// </summary>
		RoughPaving,
		/// <summary>
		/// Монолитный бетон
		/// </summary>
		MonolithicConcrete,
		/// <summary>
		/// Сборные ж.б. плиты
		/// </summary>
		FerroconcreteSlabs,
		/// <summary>
		/// Тюфяки, матрасы-рено
		/// </summary>
		Mattresses,
		/// <summary>
		/// Решетчатые ж.б. конструкции с щебеночной засыпкой
		/// </summary>
		LatticeFerroconcreteGravelBackfill,
		/// <summary>
		/// Габионы
		/// </summary>
		Gabions,
		/// <summary>
		/// Геотекстиль с щебеночной засыпкой
		/// </summary>
		GeotextileGravelBackfill,
		/// <summary>
		/// Нет укрепления
		/// </summary>
		None,
	}
}