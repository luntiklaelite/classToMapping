using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления RegulatoryStructures (Регуляционные сооружения) в строку и обратно
	/// </summary>
	public class RegulatoryStructuresStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<RegulatoryStructures>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Струенаправляющая дамба с различными видами укрепления откосов
		/// </summary>
		private static readonly string StrJetGuideSlopeReinforcement = "Струенаправляющая дамба с различными видами укрепления откосов";
		/// <summary>
		/// Струенаправляющая дамба с траверсами
		/// </summary>
		private static readonly string StrJetGuideWithTraverses = "Струенаправляющая дамба с траверсами";
		/// <summary>
		/// Укрепление берега различными конструкциями
		/// </summary>
		private static readonly string StrStrengtheningCoastVariousStructures = "Укрепление берега различными конструкциями";
		/// <summary>
		/// Струенаправляющая дамба и укрепление берега
		/// </summary>
		private static readonly string StrJetGuideShoreReinforcement = "Струенаправляющая дамба и укрепление берега";
		/// <summary>
		/// Конус
		/// </summary>
		private static readonly string StrCone = "Конус";
		/// <summary>
		/// Подпорная или заборная стенка
		/// </summary>
		private static readonly string StrRetainingOrFenceWall = "Подпорная или заборная стенка";
		/// <summary>
		/// Регуляционных сооружений нет
		/// </summary>
		private static readonly string StrNone = "Регуляционных сооружений нет";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public RegulatoryStructuresStrings() { }
		private static RegulatoryStructuresStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static RegulatoryStructuresStrings Instance
			=> instance ?? (instance = new RegulatoryStructuresStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления RegulatoryStructures (Регуляционные сооружения)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(RegulatoryStructures enumElement)
		{
			switch(enumElement)
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
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления RegulatoryStructures (Регуляционные сооружения) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
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
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}