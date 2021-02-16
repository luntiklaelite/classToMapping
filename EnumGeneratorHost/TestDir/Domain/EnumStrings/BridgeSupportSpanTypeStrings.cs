using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления BridgeSupportSpanType (Опорная часть) в строку и обратно
	/// </summary>
	public class BridgeSupportSpanTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<BridgeSupportSpanType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Эластомерная
		/// </summary>
		private static readonly string StrElastomeric = "Эластомерная";
		/// <summary>
		/// Металлическая
		/// </summary>
		private static readonly string StrMetal = "Металлическая";
		/// <summary>
		/// Комбинированная
		/// </summary>
		private static readonly string StrCombined = "Комбинированная";
		/// <summary>
		/// Прочее
		/// </summary>
		private static readonly string StrOther = "Прочее";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public BridgeSupportSpanTypeStrings() { }
		private static BridgeSupportSpanTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static BridgeSupportSpanTypeStrings Instance
			=> instance ?? (instance = new BridgeSupportSpanTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления BridgeSupportSpanType (Опорная часть)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(BridgeSupportSpanType enumElement)
		{
			switch(enumElement)
			{
				case BridgeSupportSpanType.Elastomeric:
					return StrElastomeric;
				case BridgeSupportSpanType.Metal:
					return StrMetal;
				case BridgeSupportSpanType.Combined:
					return StrCombined;
				case BridgeSupportSpanType.Other:
					return StrOther;
				case BridgeSupportSpanType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления BridgeSupportSpanType (Опорная часть) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public BridgeSupportSpanType GetElement(string name)
		{
			if (name.Equals(StrElastomeric, StringComparison.OrdinalIgnoreCase))
				return BridgeSupportSpanType.Elastomeric;
			if (name.Equals(StrMetal, StringComparison.OrdinalIgnoreCase))
				return BridgeSupportSpanType.Metal;
			if (name.Equals(StrCombined, StringComparison.OrdinalIgnoreCase))
				return BridgeSupportSpanType.Combined;
			if (name.Equals(StrOther, StringComparison.OrdinalIgnoreCase))
				return BridgeSupportSpanType.Other;
			if (name.Equals(StrNoData, StringComparison.OrdinalIgnoreCase))
				return BridgeSupportSpanType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}