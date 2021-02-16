using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления ProtectionType (Тип ограждения) в строку и обратно
	/// </summary>
	public class ProtectionTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<ProtectionType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Парапетное
		/// </summary>
		private static readonly string StrParapet = "Парапетное";
		/// <summary>
		/// Барьерное
		/// </summary>
		private static readonly string StrBarrier = "Барьерное";
		/// <summary>
		/// Бордюрное
		/// </summary>
		private static readonly string StrBorder = "Бордюрное";
		/// <summary>
		/// Тросовое
		/// </summary>
		private static readonly string StrCable = "Тросовое";
		/// <summary>
		/// Комбинированное
		/// </summary>
		private static readonly string StrCombined = "Комбинированное";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public ProtectionTypeStrings() { }
		private static ProtectionTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static ProtectionTypeStrings Instance
			=> instance ?? (instance = new ProtectionTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления ProtectionType (Тип ограждения)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(ProtectionType enumElement)
		{
			switch(enumElement)
			{
				case ProtectionType.Parapet:
					return StrParapet;
				case ProtectionType.Barrier:
					return StrBarrier;
				case ProtectionType.Border:
					return StrBorder;
				case ProtectionType.Cable:
					return StrCable;
				case ProtectionType.Combined:
					return StrCombined;
				case ProtectionType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления ProtectionType (Тип ограждения) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public ProtectionType GetElement(string name)
		{
			if (name == StrParapet)
				return ProtectionType.Parapet;
			if (name == StrBarrier)
				return ProtectionType.Barrier;
			if (name == StrBorder)
				return ProtectionType.Border;
			if (name == StrCable)
				return ProtectionType.Cable;
			if (name == StrCombined)
				return ProtectionType.Combined;
			if (name == StrNoData)
				return ProtectionType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}