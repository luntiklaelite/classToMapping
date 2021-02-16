using ITS.Core.Bridges.Domain.Enums;
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления Test1 (Тест1) в строку и обратно
	/// </summary>
	public class Test1Strings : ITS.Core.Bridges.Domain.Base.IEnumStrings<Test1>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Тест1
		/// </summary>
		private static readonly string StrTest1 = "Тест1";
		/// <summary>
		/// Тест1
		/// </summary>
		private static readonly string StrTest1 = "Тест1";
		/// <summary>
		/// Тест1
		/// </summary>
		private static readonly string StrTest1 = "Тест1";
		/// <summary>
		/// Тест1
		/// </summary>
		private static readonly string StrTest1 = "Тест1";
		/// <summary>
		/// Тест1
		/// </summary>
		private static readonly string StrTest1 = "Тест1";
		private static readonly StringBuilder stringBuilder = new StringBuilder();

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public Test1Strings() { }
		private static Test1Strings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static Test1Strings Instance
			=> instance ?? (instance = new Test1Strings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления Test1 (Тест1)
		/// </summary>
		/// <param name="test1">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(Test1 test1)
		{
			stringBuilder.Clear();
			bool first = true;
			if(test1 == Test1.NoData)
			{
				return "Нет данных";
			}
			if ((test1 & Test1.Test1) == Test1.Test1)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest1);
				first = false;
			}
			if ((test1 & Test1.Test1) == Test1.Test1)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest1);
				first = false;
			}
			if ((test1 & Test1.Test1) == Test1.Test1)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest1);
				first = false;
			}
			if ((test1 & Test1.Test1) == Test1.Test1)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest1);
				first = false;
			}
			if ((test1 & Test1.Test1) == Test1.Test1)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest1);
				first = false;
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Метод для элемента перечисления Test1 (Тест1) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public Test1 GetElement(string name)
		{
			var tmp = name.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			var res = Test1.NoData;
			foreach (var item in tmp)
			{
				if (item.Equals(StrTest1, StringComparison.OrdinalIgnoreCase))
					res |= Test1.Test1;
				else if (item.Equals(StrTest1, StringComparison.OrdinalIgnoreCase))
					res |= Test1.Test1;
				else if (item.Equals(StrTest1, StringComparison.OrdinalIgnoreCase))
					res |= Test1.Test1;
				else if (item.Equals(StrTest1, StringComparison.OrdinalIgnoreCase))
					res |= Test1.Test1;
				else if (item.Equals(StrTest1, StringComparison.OrdinalIgnoreCase))
					res |= Test1.Test1;
			}
			return res;
		}
	}
}