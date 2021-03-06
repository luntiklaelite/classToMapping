using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Регуляционные сооружения
	/// </summary>
	[Serializable]
	public enum RegulatoryStructures : byte
	{
		/// <summary>
		/// Струенаправляющая дамба с различными видами укрепления откосов
		/// </summary>
		JetGuideSlopeReinforcement,
		/// <summary>
		/// Струенаправляющая дамба с траверсами
		/// </summary>
		JetGuideWithTraverses,
		/// <summary>
		/// Укрепление берега различными конструкциями
		/// </summary>
		StrengtheningCoastVariousStructures,
		/// <summary>
		/// Струенаправляющая дамба и укрепление берега
		/// </summary>
		JetGuideShoreReinforcement,
		/// <summary>
		/// Конус
		/// </summary>
		Cone,
		/// <summary>
		/// Подпорная или заборная стенка
		/// </summary>
		RetainingOrFenceWall,
		/// <summary>
		/// Регуляционных сооружений нет
		/// </summary>
		None,
	}
}