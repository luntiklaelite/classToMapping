//Generated by EnumGenerator, 26.08.2020 11:39:27
using ITS.Core.Bridges.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления CoverType в строку и обратно
	/// </summary>
	public class CoverTypeStrings : IEnumStrings<CoverType>
	{
		private static readonly string StrAsphaltConcrete = "Асфальтобетон";
		private static readonly string StrCementConcrete = "Цементобетон";
		private static readonly string StrBlackRubble = "Черный щебень";
		private static readonly string StrStonePavement = "Каменная мостовая";
		private static readonly string StrBoardwalk = "Дощатый настил";
		private static readonly string StrSandAndGravel = "Песчано-гравийная смесь";
		private static readonly string StrGround = "Грунтовое";
		private static readonly string StrFerroconcreteSlabsCementSand = "Ж.б. плиты по цементно-песчаной подготовке";

		public CoverTypeStrings() { }
		private static CoverTypeStrings instance;
		public static CoverTypeStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new CoverTypeStrings();
				}
				return instance;
			}
		}

		public string GetName(CoverType cover_type)
		{
			switch(cover_type)
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
				default:
					break;
			}
			return null;
		}

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
			return CoverType.FerroconcreteSlabsCementSand;
		}
	}
}
