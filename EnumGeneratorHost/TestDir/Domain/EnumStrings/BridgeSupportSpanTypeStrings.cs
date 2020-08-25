//Generated by EnumGenerator, 25.08.2020 10:59:08
using ITS.Core.Bridges.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления BridgeSupportSpanType в строку и обратно
	/// </summary>
	public class BridgeSupportSpanTypeStrings : IEnumStrings<BridgeSupportSpanType>
	{
		 public static Dictionary<BridgeSupportSpanType, string> Strings =
			 new Dictionary<BridgeSupportSpanType, string> {
				 { BridgeSupportSpanType.Elastomeric,"Эластомерная" },
				 { BridgeSupportSpanType.Metal,"Металлическая" },
				 { BridgeSupportSpanType.Combined,"Комбинированная" },
				 { BridgeSupportSpanType.Other,"Прочее" },
				 { BridgeSupportSpanType.NoData,"Нет данных" },
			};
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
			return Strings[bridge_support_span_type];
		}
		public BridgeSupportSpanType GetElement(string name)
		{
			return Strings.FirstOrDefault(s => s.Value == name).Key;
		}
	}
}
