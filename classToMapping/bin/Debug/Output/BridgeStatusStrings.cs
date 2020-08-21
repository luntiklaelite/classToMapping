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
				 { BridgeStatus.BridgeStatus,"Статус объекта" },
				 { BridgeStatus.Set,"Установлен" },
				 { BridgeStatus.Required,"Требуется" },
				 { BridgeStatus.Dismantle,"Демонтировать" },
				 { BridgeStatus.Repairs,"Ремонт" },
				 { BridgeStatus.Temporary,"Временный" },
			};
		private static BridgeStatusStrings instance;
		public BridgeStatusStrings() { }
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
		public BridgeStatus GetName(string name)
		{
			return Strings.FirstOrDefault(s => s.Value == name).Key;
		}
	}
}
