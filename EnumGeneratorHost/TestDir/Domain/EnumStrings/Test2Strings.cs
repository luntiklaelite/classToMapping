using ITS.Core.Bridges.Domain.Enums;
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления Test2 (Тест2) в строку и обратно
	/// </summary>
	public class Test2Strings : ITS.Core.Bridges.Domain.Base.IEnumStrings<Test2>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Тест2
		/// </summary>
		private static readonly string StrTest2 = "Тест2";
		/// <summary>
		/// Тест2
		/// </summary>
		private static readonly string StrTest2 = "Тест2";
		/// <summary>
		/// Тест2
		/// </summary>
		private static readonly string StrTest2 = "Тест2";
		private static readonly StringBuilder stringBuilder = new StringBuilder();

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public Test2Strings() { }
		private static Test2Strings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static Test2Strings Instance
			=> instance ?? (instance = new Test2Strings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления Test2 (Тест2)
		/// </summary>
		/// <param name="test2">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(Test2 test2)
		{
			stringBuilder.Clear();
			bool first = true;
			if(test2 == Test2.NoData)
			{
				return "Нет данных";
			}
			if ((test2 & Test2.Test2) == Test2.Test2)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest2);
				first = false;
			}
			if ((test2 & Test2.Test2) == Test2.Test2)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest2);
				first = false;
			}
			if ((test2 & Test2.Test2) == Test2.Test2)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrTest2);
				first = false;
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Метод для элемента перечисления Test2 (Тест2) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public Test2 GetElement(string name)
		{
			var tmp = name.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			var res = Test2.NoData;
			foreach (var item in tmp)
			{
				if (item == StrTest2)
					res |= Test2.Test2;
				else if (item == StrTest2)
					res |= Test2.Test2;
				else if (item == StrTest2)
					res |= Test2.Test2;
			}
			return res;
		}
	}
}