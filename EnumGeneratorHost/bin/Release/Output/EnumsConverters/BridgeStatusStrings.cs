//Generated by EnumGenerator, 23.08.2020 19:55:32
using ITS.Core.Bridges.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.EnumString
{
	/// <summary>
	/// Класс-преобразователь перечисления BridgeStatus в строку и обратно
	/// </summary>
	public class BridgeStatusStrings : IEnumStrings<BridgeStatus>
	{
		 public static Dictionary<BridgeStatus, string> Strings =
			 new Dictionary<BridgeStatus, string> {
				 { BridgeStatus.Set,"Установлен" },
				 { BridgeStatus.Required,"Требуется" },
				 { BridgeStatus.Dismantle,"Демонтировать" },
				 { BridgeStatus.Repairs,"Ремонт" },
				 { BridgeStatus.Temporary,"Временный" },
			};
		public BridgeStatusStrings() { }
		private static BridgeStatusStrings instance;
		public static BridgeStatusStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BridgeStatusStrings();
				}
				return instance;
			}
		}

		public string GetName(BridgeStatus bridge_status)
		{
			return Strings[bridge_status];
		}
		public BridgeStatus GetElement(string name)
		{
			return Strings.FirstOrDefault(s => s.Value == name).Key;
		}
	}
}
