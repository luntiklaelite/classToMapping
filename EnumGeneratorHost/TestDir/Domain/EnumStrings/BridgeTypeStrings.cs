using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления BridgeType (Тип сооружения) в строку и обратно
	/// </summary>
	public class BridgeTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<BridgeType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Путепровод
		/// </summary>
		private static readonly string StrOverpass = "Путепровод";
		/// <summary>
		/// Эстакада
		/// </summary>
		private static readonly string StrEstacada = "Эстакада";
		/// <summary>
		/// Скотопрогон
		/// </summary>
		private static readonly string StrCattleDrive = "Скотопрогон";
		/// <summary>
		/// Понтон
		/// </summary>
		private static readonly string StrPontoon = "Понтон";
		/// <summary>
		/// Засыпного типа
		/// </summary>
		private static readonly string StrFillType = "Засыпного типа";
		/// <summary>
		/// Виадук
		/// </summary>
		private static readonly string StrViaduct = "Виадук";
		/// <summary>
		/// Акведук
		/// </summary>
		private static readonly string StrAqueduct = "Акведук";
		/// <summary>
		/// Летающий паром
		/// </summary>
		private static readonly string StrFlyingFerry = "Летающий паром";
		/// <summary>
		/// Разводной мост
		/// </summary>
		private static readonly string StrDrawbridge = "Разводной мост";
		/// <summary>
		/// Мост
		/// </summary>
		private static readonly string StrBridge = "Мост";
		/// <summary>
		/// Тоннель
		/// </summary>
		private static readonly string StrTunnel = "Тоннель";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public BridgeTypeStrings() { }
		private static BridgeTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static BridgeTypeStrings Instance
			=> instance ?? (instance = new BridgeTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления BridgeType (Тип сооружения)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(BridgeType enumElement)
		{
			switch(enumElement)
			{
				case BridgeType.Overpass:
					return StrOverpass;
				case BridgeType.Estacada:
					return StrEstacada;
				case BridgeType.CattleDrive:
					return StrCattleDrive;
				case BridgeType.Pontoon:
					return StrPontoon;
				case BridgeType.FillType:
					return StrFillType;
				case BridgeType.Viaduct:
					return StrViaduct;
				case BridgeType.Aqueduct:
					return StrAqueduct;
				case BridgeType.FlyingFerry:
					return StrFlyingFerry;
				case BridgeType.Drawbridge:
					return StrDrawbridge;
				case BridgeType.Bridge:
					return StrBridge;
				case BridgeType.Tunnel:
					return StrTunnel;
				case BridgeType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления BridgeType (Тип сооружения) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public BridgeType GetElement(string name)
		{
			if (name.Equals(StrOverpass, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Overpass;
			if (name.Equals(StrEstacada, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Estacada;
			if (name.Equals(StrCattleDrive, StringComparison.OrdinalIgnoreCase))
				return BridgeType.CattleDrive;
			if (name.Equals(StrPontoon, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Pontoon;
			if (name.Equals(StrFillType, StringComparison.OrdinalIgnoreCase))
				return BridgeType.FillType;
			if (name.Equals(StrViaduct, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Viaduct;
			if (name.Equals(StrAqueduct, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Aqueduct;
			if (name.Equals(StrFlyingFerry, StringComparison.OrdinalIgnoreCase))
				return BridgeType.FlyingFerry;
			if (name.Equals(StrDrawbridge, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Drawbridge;
			if (name.Equals(StrBridge, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Bridge;
			if (name.Equals(StrTunnel, StringComparison.OrdinalIgnoreCase))
				return BridgeType.Tunnel;
			if (name.Equals(StrNoData, StringComparison.OrdinalIgnoreCase))
				return BridgeType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}