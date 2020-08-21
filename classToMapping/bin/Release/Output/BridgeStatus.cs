using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Статус объекта
	/// </summary>
	[Serializable]
	public enum BridgeStatus
	{
		/// <summary>
		/// Статус объекта
		/// </summary>
		BridgeStatus,
		/// <summary>
		/// Установлен
		/// </summary>
		Set,
		/// <summary>
		/// Требуется
		/// </summary>
		Required,
		/// <summary>
		/// Демонтировать
		/// </summary>
		Dismantle,
		/// <summary>
		/// Ремонт
		/// </summary>
		Repairs,
		/// <summary>
		/// Временный
		/// </summary>
		Temporary,
	}
}
