using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления Strenghtening (Укрепление) в строку и обратно
	/// </summary>
	public class StrenghteningStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<Strenghtening>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Одерновка
		/// </summary>
		private static readonly string StrOdernovka = "Одерновка";
		/// <summary>
		/// Каменная наброска, мощение
		/// </summary>
		private static readonly string StrRoughPaving = "Каменная наброска, мощение";
		/// <summary>
		/// Монолитный бетон
		/// </summary>
		private static readonly string StrMonolithicConcrete = "Монолитный бетон";
		/// <summary>
		/// Сборные ж.б. плиты
		/// </summary>
		private static readonly string StrFerroconcreteSlabs = "Сборные ж.б. плиты";
		/// <summary>
		/// Тюфяки, матрасы-рено
		/// </summary>
		private static readonly string StrMattresses = "Тюфяки, матрасы-рено";
		/// <summary>
		/// Решетчатые ж.б. конструкции с щебеночной засыпкой
		/// </summary>
		private static readonly string StrLatticeFerroconcreteGravelBackfill = "Решетчатые ж.б. конструкции с щебеночной засыпкой";
		/// <summary>
		/// Габионы
		/// </summary>
		private static readonly string StrGabions = "Габионы";
		/// <summary>
		/// Геотекстиль с щебеночной засыпкой
		/// </summary>
		private static readonly string StrGeotextileGravelBackfill = "Геотекстиль с щебеночной засыпкой";
		/// <summary>
		/// Нет укрепления
		/// </summary>
		private static readonly string StrNone = "Нет укрепления";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public StrenghteningStrings() { }
		private static StrenghteningStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static StrenghteningStrings Instance
			=> instance ?? (instance = new StrenghteningStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления Strenghtening (Укрепление)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(Strenghtening enumElement)
		{
			switch(enumElement)
			{
				case Strenghtening.Odernovka:
					return StrOdernovka;
				case Strenghtening.RoughPaving:
					return StrRoughPaving;
				case Strenghtening.MonolithicConcrete:
					return StrMonolithicConcrete;
				case Strenghtening.FerroconcreteSlabs:
					return StrFerroconcreteSlabs;
				case Strenghtening.Mattresses:
					return StrMattresses;
				case Strenghtening.LatticeFerroconcreteGravelBackfill:
					return StrLatticeFerroconcreteGravelBackfill;
				case Strenghtening.Gabions:
					return StrGabions;
				case Strenghtening.GeotextileGravelBackfill:
					return StrGeotextileGravelBackfill;
				case Strenghtening.None:
					return StrNone;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления Strenghtening (Укрепление) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public Strenghtening GetElement(string name)
		{
			if (name == StrOdernovka)
				return Strenghtening.Odernovka;
			if (name == StrRoughPaving)
				return Strenghtening.RoughPaving;
			if (name == StrMonolithicConcrete)
				return Strenghtening.MonolithicConcrete;
			if (name == StrFerroconcreteSlabs)
				return Strenghtening.FerroconcreteSlabs;
			if (name == StrMattresses)
				return Strenghtening.Mattresses;
			if (name == StrLatticeFerroconcreteGravelBackfill)
				return Strenghtening.LatticeFerroconcreteGravelBackfill;
			if (name == StrGabions)
				return Strenghtening.Gabions;
			if (name == StrGeotextileGravelBackfill)
				return Strenghtening.GeotextileGravelBackfill;
			if (name == StrNone)
				return Strenghtening.None;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}