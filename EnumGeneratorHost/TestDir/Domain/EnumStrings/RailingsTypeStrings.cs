using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления RailingsType (Тип перил) в строку и обратно
	/// </summary>
	public class RailingsTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<RailingsType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Перила отсутствуют
		/// </summary>
		private static readonly string StrNone = "Перила отсутствуют";
		/// <summary>
		/// Металлические (секционные или непрерывные)
		/// </summary>
		private static readonly string StrMetallic = "Металлические (секционные или непрерывные)";
		/// <summary>
		/// Железобетонный поручень с металлической решеткой
		/// </summary>
		private static readonly string StrFerroncreteMetalGrating = "Железобетонный поручень с металлической решеткой";
		/// <summary>
		/// Железобетонные (решетчатые или со сплошной стенкой)
		/// </summary>
		private static readonly string StrFerroconcreteLlatticeOrSolidWall = "Железобетонные (решетчатые или со сплошной стенкой)";
		/// <summary>
		/// Деревянные
		/// </summary>
		private static readonly string StrWood = "Деревянные";
		/// <summary>
		/// Комбинированные
		/// </summary>
		private static readonly string StrCombined = "Комбинированные";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public RailingsTypeStrings() { }
		private static RailingsTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static RailingsTypeStrings Instance
			=> instance ?? (instance = new RailingsTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления RailingsType (Тип перил)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(RailingsType enumElement)
		{
			switch(enumElement)
			{
				case RailingsType.None:
					return StrNone;
				case RailingsType.Metallic:
					return StrMetallic;
				case RailingsType.FerroncreteMetalGrating:
					return StrFerroncreteMetalGrating;
				case RailingsType.FerroconcreteLlatticeOrSolidWall:
					return StrFerroconcreteLlatticeOrSolidWall;
				case RailingsType.Wood:
					return StrWood;
				case RailingsType.Combined:
					return StrCombined;
				case RailingsType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления RailingsType (Тип перил) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public RailingsType GetElement(string name)
		{
			if (name == StrNone)
				return RailingsType.None;
			if (name == StrMetallic)
				return RailingsType.Metallic;
			if (name == StrFerroncreteMetalGrating)
				return RailingsType.FerroncreteMetalGrating;
			if (name == StrFerroconcreteLlatticeOrSolidWall)
				return RailingsType.FerroconcreteLlatticeOrSolidWall;
			if (name == StrWood)
				return RailingsType.Wood;
			if (name == StrCombined)
				return RailingsType.Combined;
			if (name == StrNoData)
				return RailingsType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}