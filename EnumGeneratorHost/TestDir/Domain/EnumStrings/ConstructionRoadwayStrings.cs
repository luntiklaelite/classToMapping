using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления ConstructionRoadway (Конструкция проезжей части) в строку и обратно
	/// </summary>
	public class ConstructionRoadwayStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<ConstructionRoadway>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Железобетонная плита в составе основной несущей железобетонной конструкции
		/// </summary>
		private static readonly string StrFerroconcreteSlabInMainSupStruct = "Железобетонная плита в составе основной несущей железобетонной конструкции";
		/// <summary>
		/// Железобетонная плита, включенная в совместную работу с металлическими главными балками (в сталежелезобетонных пролетных строениях)
		/// </summary>
		private static readonly string StrFerroconcreteSlabWithPiles = "Железобетонная плита, включенная в совместную работу с металлическими главными балками (в сталежелезобетонных пролетных строениях)";
		/// <summary>
		/// Железобетонная плита по балкам без объединения
		/// </summary>
		private static readonly string StrFerroconcreteSlabOnPiles = "Железобетонная плита по балкам без объединения";
		/// <summary>
		/// Ортотропная плита в составе главных и поперечных балок
		/// </summary>
		private static readonly string StrOrthotropicSlab = "Ортотропная плита в составе главных и поперечных балок";
		/// <summary>
		/// Деревянная
		/// </summary>
		private static readonly string StrWood = "Деревянная";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public ConstructionRoadwayStrings() { }
		private static ConstructionRoadwayStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static ConstructionRoadwayStrings Instance
			=> instance ?? (instance = new ConstructionRoadwayStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления ConstructionRoadway (Конструкция проезжей части)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(ConstructionRoadway enumElement)
		{
			switch(enumElement)
			{
				case ConstructionRoadway.FerroconcreteSlabInMainSupStruct:
					return StrFerroconcreteSlabInMainSupStruct;
				case ConstructionRoadway.FerroconcreteSlabWithPiles:
					return StrFerroconcreteSlabWithPiles;
				case ConstructionRoadway.FerroconcreteSlabOnPiles:
					return StrFerroconcreteSlabOnPiles;
				case ConstructionRoadway.OrthotropicSlab:
					return StrOrthotropicSlab;
				case ConstructionRoadway.Wood:
					return StrWood;
				case ConstructionRoadway.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления ConstructionRoadway (Конструкция проезжей части) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public ConstructionRoadway GetElement(string name)
		{
			if (name == StrFerroconcreteSlabInMainSupStruct)
				return ConstructionRoadway.FerroconcreteSlabInMainSupStruct;
			if (name == StrFerroconcreteSlabWithPiles)
				return ConstructionRoadway.FerroconcreteSlabWithPiles;
			if (name == StrFerroconcreteSlabOnPiles)
				return ConstructionRoadway.FerroconcreteSlabOnPiles;
			if (name == StrOrthotropicSlab)
				return ConstructionRoadway.OrthotropicSlab;
			if (name == StrWood)
				return ConstructionRoadway.Wood;
			if (name == StrNoData)
				return ConstructionRoadway.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}