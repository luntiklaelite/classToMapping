//Generated by EnumGenerator, 02.09.2020 11:38:31
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления BridgeSupportSpanType в строку и обратно
	/// </summary>
	public class BridgeSupportSpanTypeStrings : IEnumStrings<BridgeSupportSpanType>
	{
		private static readonly string StrElastomeric = "Эластомерная";
		private static readonly string StrMetal = "Металлическая";
		private static readonly string StrCombined = "Комбинированная";
		private static readonly string StrOther = "Прочее";
		private static readonly string StrNoData = "Нет данных";

		public BridgeSupportSpanTypeStrings() { }
		private static BridgeSupportSpanTypeStrings instance;
		public static BridgeSupportSpanTypeStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BridgeSupportSpanTypeStrings();
				}
				return instance;
			}
		}

		public string GetName(BridgeSupportSpanType bridge_support_span_type)
		{
			switch(bridge_support_span_type)
			{
				case BridgeSupportSpanType.Elastomeric:
					return StrElastomeric;
				case BridgeSupportSpanType.Metal:
					return StrMetal;
				case BridgeSupportSpanType.Combined:
					return StrCombined;
				case BridgeSupportSpanType.Other:
					return StrOther;
				case BridgeSupportSpanType.NoData:
					return StrNoData;
				default:
					break;
			}
			return null;
		}

		public BridgeSupportSpanType GetElement(string name)
		{
			if (name == StrElastomeric)
				return BridgeSupportSpanType.Elastomeric;
			if (name == StrMetal)
				return BridgeSupportSpanType.Metal;
			if (name == StrCombined)
				return BridgeSupportSpanType.Combined;
			if (name == StrOther)
				return BridgeSupportSpanType.Other;
			if (name == StrNoData)
				return BridgeSupportSpanType.NoData;
			return BridgeSupportSpanType.NoData;
		}
	}
}
