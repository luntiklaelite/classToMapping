using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления CoverType (Тип покрытия проезжей части) в строку и обратно
	/// </summary>
	public class CoverTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<CoverType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Асфальтобетон
		/// </summary>
		private static readonly string StrAsphaltConcrete = "Асфальтобетон";
		/// <summary>
		/// Цементобетон
		/// </summary>
		private static readonly string StrCementConcrete = "Цементобетон";
		/// <summary>
		/// Черный щебень
		/// </summary>
		private static readonly string StrBlackRubble = "Черный щебень";
		/// <summary>
		/// Каменная мостовая
		/// </summary>
		private static readonly string StrStonePavement = "Каменная мостовая";
		/// <summary>
		/// Дощатый настил
		/// </summary>
		private static readonly string StrBoardwalk = "Дощатый настил";
		/// <summary>
		/// Песчано-гравийная смесь
		/// </summary>
		private static readonly string StrSandAndGravel = "Песчано-гравийная смесь";
		/// <summary>
		/// Грунтовое
		/// </summary>
		private static readonly string StrGround = "Грунтовое";
		/// <summary>
		/// Ж.б. плиты по цементно-песчаной подготовке
		/// </summary>
		private static readonly string StrFerroconcreteSlabsCementSand = "Ж.б. плиты по цементно-песчаной подготовке";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public CoverTypeStrings() { }
		private static CoverTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static CoverTypeStrings Instance
			=> instance ?? (instance = new CoverTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления CoverType (Тип покрытия проезжей части)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(CoverType enumElement)
		{
			switch(enumElement)
			{
				case CoverType.AsphaltConcrete:
					return StrAsphaltConcrete;
				case CoverType.CementConcrete:
					return StrCementConcrete;
				case CoverType.BlackRubble:
					return StrBlackRubble;
				case CoverType.StonePavement:
					return StrStonePavement;
				case CoverType.Boardwalk:
					return StrBoardwalk;
				case CoverType.SandAndGravel:
					return StrSandAndGravel;
				case CoverType.Ground:
					return StrGround;
				case CoverType.FerroconcreteSlabsCementSand:
					return StrFerroconcreteSlabsCementSand;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления CoverType (Тип покрытия проезжей части) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public CoverType GetElement(string name)
		{
			if (name == StrAsphaltConcrete)
				return CoverType.AsphaltConcrete;
			if (name == StrCementConcrete)
				return CoverType.CementConcrete;
			if (name == StrBlackRubble)
				return CoverType.BlackRubble;
			if (name == StrStonePavement)
				return CoverType.StonePavement;
			if (name == StrBoardwalk)
				return CoverType.Boardwalk;
			if (name == StrSandAndGravel)
				return CoverType.SandAndGravel;
			if (name == StrGround)
				return CoverType.Ground;
			if (name == StrFerroconcreteSlabsCementSand)
				return CoverType.FerroconcreteSlabsCementSand;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}