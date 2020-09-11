//Generated by EnumGenerator, 02.09.2020 13:21:24
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления RegulatoryStructures в строку и обратно
	/// </summary>
	public class RegulatoryStructuresStrings : IEnumStrings<RegulatoryStructures>
	{
		private static readonly string StrJetGuideSlopeReinforcement = "Струенаправляющая дамба с различными видами укрепления откосов";
		private static readonly string StrJetGuideWithTraverses = "Струенаправляющая дамба с траверсами";
		private static readonly string StrStrengtheningCoastVariousStructures = "Укрепление берега различными конструкциями";
		private static readonly string StrJetGuideShoreReinforcement = "Струенаправляющая дамба и укрепление берега";
		private static readonly string StrCone = "Конус";
		private static readonly string StrRetainingOrFenceWall = "Подпорная или заборная стенка";
		private static readonly string StrNone = "Регуляционных сооружений нет";

		public RegulatoryStructuresStrings() { }
		private static RegulatoryStructuresStrings instance;
		public static RegulatoryStructuresStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new RegulatoryStructuresStrings();
				}
				return instance;
			}
		}

		public string GetName(RegulatoryStructures regulatory_structures)
		{
			switch(regulatory_structures)
			{
				case RegulatoryStructures.JetGuideSlopeReinforcement:
					return StrJetGuideSlopeReinforcement;
				case RegulatoryStructures.JetGuideWithTraverses:
					return StrJetGuideWithTraverses;
				case RegulatoryStructures.StrengtheningCoastVariousStructures:
					return StrStrengtheningCoastVariousStructures;
				case RegulatoryStructures.JetGuideShoreReinforcement:
					return StrJetGuideShoreReinforcement;
				case RegulatoryStructures.Cone:
					return StrCone;
				case RegulatoryStructures.RetainingOrFenceWall:
					return StrRetainingOrFenceWall;
				case RegulatoryStructures.None:
					return StrNone;
				default:
					break;
			}
			return null;
		}

		public RegulatoryStructures GetElement(string name)
		{
			if (name == StrJetGuideSlopeReinforcement)
				return RegulatoryStructures.JetGuideSlopeReinforcement;
			if (name == StrJetGuideWithTraverses)
				return RegulatoryStructures.JetGuideWithTraverses;
			if (name == StrStrengtheningCoastVariousStructures)
				return RegulatoryStructures.StrengtheningCoastVariousStructures;
			if (name == StrJetGuideShoreReinforcement)
				return RegulatoryStructures.JetGuideShoreReinforcement;
			if (name == StrCone)
				return RegulatoryStructures.Cone;
			if (name == StrRetainingOrFenceWall)
				return RegulatoryStructures.RetainingOrFenceWall;
			if (name == StrNone)
				return RegulatoryStructures.None;
			return RegulatoryStructures.None;
		}
	}
}
