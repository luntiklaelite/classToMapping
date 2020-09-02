//Generated by EnumGenerator, 02.09.2020 11:38:32
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления Strenghtening в строку и обратно
	/// </summary>
	public class StrenghteningStrings : IEnumStrings<Strenghtening>
	{
		private static readonly string StrOdernovka = "Одерновка";
		private static readonly string StrRoughPaving = "Каменная наброска, мощение";
		private static readonly string StrMonolithicConcrete = "Монолитный бетон";
		private static readonly string StrFerroconcreteSlabs = "Сборные ж.б. плиты";
		private static readonly string StrMattresses = "Тюфяки, матрасы-рено";
		private static readonly string StrLatticeFerroconcreteGravelBackfill = "Решетчатые ж.б. конструкции с щебеночной засыпкой";
		private static readonly string StrGabions = "Габионы";
		private static readonly string StrGeotextileGravelBackfill = "Геотекстиль с щебеночной засыпкой";
		private static readonly string StrNone = "Нет укрепления";

		public StrenghteningStrings() { }
		private static StrenghteningStrings instance;
		public static StrenghteningStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new StrenghteningStrings();
				}
				return instance;
			}
		}

		public string GetName(Strenghtening strenghtening)
		{
			switch(strenghtening)
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
				default:
					break;
			}
			return null;
		}

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
			return Strenghtening.None;
		}
	}
}
