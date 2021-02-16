using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления CrossJoinMethod (Cпособ поперечного объединения) в строку и обратно
	/// </summary>
	public class CrossJoinMethodStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<CrossJoinMethod>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Не объединены
		/// </summary>
		private static readonly string StrNone = "Не объединены";
		/// <summary>
		/// По шпонкам
		/// </summary>
		private static readonly string StrDowels = "По шпонкам";
		/// <summary>
		/// По диафрагмам
		/// </summary>
		private static readonly string StrDiaphragms = "По диафрагмам";
		/// <summary>
		/// По плите
		/// </summary>
		private static readonly string StrStove = "По плите";
		/// <summary>
		/// По плите и диафрагмам
		/// </summary>
		private static readonly string StrPlateAndDiaphragms = "По плите и диафрагмам";
		/// <summary>
		/// По поперечным балкам и связям
		/// </summary>
		private static readonly string StrTransverseBeamsAndTies = "По поперечным балкам и связям";
		/// <summary>
		/// По продольным и поперечным связям
		/// </summary>
		private static readonly string StrLongitudinalAndTransverseLinks = "По продольным и поперечным связям";
		/// <summary>
		/// По плите и поперечным связям
		/// </summary>
		private static readonly string StrSlabAndCrossBraces = "По плите и поперечным связям";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public CrossJoinMethodStrings() { }
		private static CrossJoinMethodStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static CrossJoinMethodStrings Instance
			=> instance ?? (instance = new CrossJoinMethodStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления CrossJoinMethod (Cпособ поперечного объединения)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(CrossJoinMethod enumElement)
		{
			switch(enumElement)
			{
				case CrossJoinMethod.None:
					return StrNone;
				case CrossJoinMethod.Dowels:
					return StrDowels;
				case CrossJoinMethod.Diaphragms:
					return StrDiaphragms;
				case CrossJoinMethod.Stove:
					return StrStove;
				case CrossJoinMethod.PlateAndDiaphragms:
					return StrPlateAndDiaphragms;
				case CrossJoinMethod.TransverseBeamsAndTies:
					return StrTransverseBeamsAndTies;
				case CrossJoinMethod.LongitudinalAndTransverseLinks:
					return StrLongitudinalAndTransverseLinks;
				case CrossJoinMethod.SlabAndCrossBraces:
					return StrSlabAndCrossBraces;
				case CrossJoinMethod.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления CrossJoinMethod (Cпособ поперечного объединения) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public CrossJoinMethod GetElement(string name)
		{
			if (name == StrNone)
				return CrossJoinMethod.None;
			if (name == StrDowels)
				return CrossJoinMethod.Dowels;
			if (name == StrDiaphragms)
				return CrossJoinMethod.Diaphragms;
			if (name == StrStove)
				return CrossJoinMethod.Stove;
			if (name == StrPlateAndDiaphragms)
				return CrossJoinMethod.PlateAndDiaphragms;
			if (name == StrTransverseBeamsAndTies)
				return CrossJoinMethod.TransverseBeamsAndTies;
			if (name == StrLongitudinalAndTransverseLinks)
				return CrossJoinMethod.LongitudinalAndTransverseLinks;
			if (name == StrSlabAndCrossBraces)
				return CrossJoinMethod.SlabAndCrossBraces;
			if (name == StrNoData)
				return CrossJoinMethod.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}