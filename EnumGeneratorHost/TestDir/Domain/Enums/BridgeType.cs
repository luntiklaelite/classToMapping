using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип сооружения
	/// </summary>
	[Serializable]
	public enum BridgeType : byte
	{
		/// <summary>
		/// Путепровод
		/// </summary>
		Overpass,
		/// <summary>
		/// Эстакада
		/// </summary>
		Estacada,
		/// <summary>
		/// Скотопрогон
		/// </summary>
		CattleDrive,
		/// <summary>
		/// Понтон
		/// </summary>
		Pontoon,
		/// <summary>
		/// Засыпного типа
		/// </summary>
		FillType,
		/// <summary>
		/// Виадук
		/// </summary>
		Viaduct,
		/// <summary>
		/// Акведук
		/// </summary>
		Aqueduct,
		/// <summary>
		/// Летающий паром
		/// </summary>
		FlyingFerry,
		/// <summary>
		/// Разводной мост
		/// </summary>
		Drawbridge,
		/// <summary>
		/// Мост
		/// </summary>
		Bridge,
		/// <summary>
		/// Тоннель
		/// </summary>
		Tunnel,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}