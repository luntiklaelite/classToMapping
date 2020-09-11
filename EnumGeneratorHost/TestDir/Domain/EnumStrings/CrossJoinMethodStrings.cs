//Generated by EnumGenerator, 02.09.2020 13:21:23
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления CrossJoinMethod в строку и обратно
	/// </summary>
	public class CrossJoinMethodStrings : IEnumStrings<CrossJoinMethod>
	{
		private static readonly string StrNone = "Не объединены";
		private static readonly string StrDowels = "По шпонкам";
		private static readonly string StrDiaphragms = "По диафрагмам";
		private static readonly string StrStove = "По плите";
		private static readonly string StrPlateAndDiaphragms = "По плите и диафрагмам";
		private static readonly string StrTransverseBeamsAndTies = "По поперечным балкам и связям";
		private static readonly string StrLongitudinalAndTransverseLinks = "По продольным и поперечным связям";
		private static readonly string StrSlabAndCrossBraces = "По плите и поперечным связям";
		private static readonly string StrNoData = "Нет данных";

		public CrossJoinMethodStrings() { }
		private static CrossJoinMethodStrings instance;
		public static CrossJoinMethodStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new CrossJoinMethodStrings();
				}
				return instance;
			}
		}

		public string GetName(CrossJoinMethod cross_join_method)
		{
			switch(cross_join_method)
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
				default:
					break;
			}
			return null;
		}

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
			return CrossJoinMethod.NoData;
		}
	}
}
