using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления ExpansionJointType (Типы деформационных швов ) в строку и обратно
	/// </summary>
	public class ExpansionJointTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<ExpansionJointType>, ITS.Core.Bridges.Domain.Base.EnumStrings
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

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public ExpansionJointTypeStrings() { }
		private static ExpansionJointTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static ExpansionJointTypeStrings Instance
			=> instance ?? (instance = new ExpansionJointTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления ExpansionJointType (Типы деформационных швов )
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
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

		/// <summary>
		/// Метод для элемента перечисления ExpansionJointType (Типы деформационных швов ) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
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