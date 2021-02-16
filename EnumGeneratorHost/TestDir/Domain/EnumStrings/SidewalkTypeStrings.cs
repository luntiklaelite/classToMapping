using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления SidewalkType (Тип тротуара) в строку и обратно
	/// </summary>
	public class SidewalkTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<SidewalkType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Тротуаров нет (зоны для прохода пешеходов)
		/// </summary>
		private static readonly string StrNone = "Тротуаров нет (зоны для прохода пешеходов)";
		/// <summary>
		/// Повышенного типа из сборных типовых блоков
		/// </summary>
		private static readonly string StrIncreasedFromBlocks = "Повышенного типа из сборных типовых блоков";
		/// <summary>
		/// Повышенного типа из свай
		/// </summary>
		private static readonly string StrIncreasedFromPiles = "Повышенного типа из свай";
		/// <summary>
		/// Пониженные в уровне проезжей части из сборных плит (блоков)
		/// </summary>
		private static readonly string StrLoweredCarriagewayBlocks = "Пониженные в уровне проезжей части из сборных плит (блоков)";
		/// <summary>
		/// Пониженные в уровне проезжей части с монолитной плитой
		/// </summary>
		private static readonly string StrLoweredCarriagewayMonolithicSlab = "Пониженные в уровне проезжей части с монолитной плитой";
		/// <summary>
		/// В уровне одежды по плите проезжей части
		/// </summary>
		private static readonly string StrClothingAlongTheSlab = "В уровне одежды по плите проезжей части";
		/// <summary>
		/// Деревянные конструкции
		/// </summary>
		private static readonly string StrWood = "Деревянные конструкции";
		/// <summary>
		/// На консолях, повышенного типа
		/// </summary>
		private static readonly string StrConsolesIncreased = "На консолях, повышенного типа";
		/// <summary>
		/// На консолях, в уровне проезжей части	
		/// </summary>
		private static readonly string StrConsolesCarriageway = "На консолях, в уровне проезжей части	";
		/// <summary>
		/// На консолях, пониженного типа
		/// </summary>
		private static readonly string StrConsolesLowered = "На консолях, пониженного типа";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public SidewalkTypeStrings() { }
		private static SidewalkTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static SidewalkTypeStrings Instance
			=> instance ?? (instance = new SidewalkTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления SidewalkType (Тип тротуара)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(SidewalkType enumElement)
		{
			switch(enumElement)
			{
				case SidewalkType.None:
					return StrNone;
				case SidewalkType.IncreasedFromBlocks:
					return StrIncreasedFromBlocks;
				case SidewalkType.IncreasedFromPiles:
					return StrIncreasedFromPiles;
				case SidewalkType.LoweredCarriagewayBlocks:
					return StrLoweredCarriagewayBlocks;
				case SidewalkType.LoweredCarriagewayMonolithicSlab:
					return StrLoweredCarriagewayMonolithicSlab;
				case SidewalkType.ClothingAlongTheSlab:
					return StrClothingAlongTheSlab;
				case SidewalkType.Wood:
					return StrWood;
				case SidewalkType.ConsolesIncreased:
					return StrConsolesIncreased;
				case SidewalkType.ConsolesCarriageway:
					return StrConsolesCarriageway;
				case SidewalkType.ConsolesLowered:
					return StrConsolesLowered;
				case SidewalkType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления SidewalkType (Тип тротуара) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public SidewalkType GetElement(string name)
		{
			if (name == StrNone)
				return SidewalkType.None;
			if (name == StrIncreasedFromBlocks)
				return SidewalkType.IncreasedFromBlocks;
			if (name == StrIncreasedFromPiles)
				return SidewalkType.IncreasedFromPiles;
			if (name == StrLoweredCarriagewayBlocks)
				return SidewalkType.LoweredCarriagewayBlocks;
			if (name == StrLoweredCarriagewayMonolithicSlab)
				return SidewalkType.LoweredCarriagewayMonolithicSlab;
			if (name == StrClothingAlongTheSlab)
				return SidewalkType.ClothingAlongTheSlab;
			if (name == StrWood)
				return SidewalkType.Wood;
			if (name == StrConsolesIncreased)
				return SidewalkType.ConsolesIncreased;
			if (name == StrConsolesCarriageway)
				return SidewalkType.ConsolesCarriageway;
			if (name == StrConsolesLowered)
				return SidewalkType.ConsolesLowered;
			if (name == StrNoData)
				return SidewalkType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}