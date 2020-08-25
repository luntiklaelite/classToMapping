//Generated by EnumGenerator, 25.08.2020 10:59:08
using ITS.Core.Bridges.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления ExpansionJointType в строку и обратно
	/// </summary>
	public class ExpansionJointTypeStrings : IEnumStrings<ExpansionJointType>
	{
		 public static Dictionary<ExpansionJointType, string> Strings =
			 new Dictionary<ExpansionJointType, string> {
				 { ExpansionJointType.Opened,"Открытый" },
				 { ExpansionJointType.Closed,"Закрытый" },
				 { ExpansionJointType.Filled,"Заполненный" },
				 { ExpansionJointType.Overlapped,"Перекрытый" },
				 { ExpansionJointType.Retractable,"Откатный" },
				 { ExpansionJointType.NoData,"Нет данных" },
			};
		public ExpansionJointTypeStrings() { }
		private static ExpansionJointTypeStrings instance;
		public static ExpansionJointTypeStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ExpansionJointTypeStrings();
				}
				return instance;
			}
		}

		public string GetName(ExpansionJointType expansion_joint_type)
		{
			return Strings[expansion_joint_type];
		}
		public ExpansionJointType GetElement(string name)
		{
			return Strings.FirstOrDefault(s => s.Value == name).Key;
		}
	}
}
