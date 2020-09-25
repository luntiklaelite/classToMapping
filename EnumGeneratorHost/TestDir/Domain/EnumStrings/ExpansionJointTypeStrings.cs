//Generated by EnumGenerator, 22.09.2020 12:53:34
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления ExpansionJointType в строку и обратно
	/// </summary>
	public class ExpansionJointTypeStrings : IEnumStrings<ExpansionJointType>
	{
		/// <summary>
		/// Открытый
		/// </summary>
		private static readonly string StrOpened = "Открытый";
		/// <summary>
		/// Закрытый
		/// </summary>
		private static readonly string StrClosed = "Закрытый";
		/// <summary>
		/// Заполненный
		/// </summary>
		private static readonly string StrFilled = "Заполненный";
		/// <summary>
		/// Перекрытый
		/// </summary>
		private static readonly string StrOverlapped = "Перекрытый";
		/// <summary>
		/// Откатный
		/// </summary>
		private static readonly string StrRetractable = "Откатный";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		public ExpansionJointTypeStrings() { }
		private static ExpansionJointTypeStrings instance;
		public static ExpansionJointTypeStrings Instance
			=> instance ?? (instance = new ExpansionJointTypeStrings());

		public string GetName(ExpansionJointType enumElement)
		{
			switch(enumElement)
			{
				case ExpansionJointType.Opened:
					return StrOpened;
				case ExpansionJointType.Closed:
					return StrClosed;
				case ExpansionJointType.Filled:
					return StrFilled;
				case ExpansionJointType.Overlapped:
					return StrOverlapped;
				case ExpansionJointType.Retractable:
					return StrRetractable;
				case ExpansionJointType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		public ExpansionJointType GetElement(string name)
		{
			if (name == StrOpened)
				return ExpansionJointType.Opened;
			if (name == StrClosed)
				return ExpansionJointType.Closed;
			if (name == StrFilled)
				return ExpansionJointType.Filled;
			if (name == StrOverlapped)
				return ExpansionJointType.Overlapped;
			if (name == StrRetractable)
				return ExpansionJointType.Retractable;
			if (name == StrNoData)
				return ExpansionJointType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}
