using ITS.Core.Bridges.Domain.Enums;
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления Arrangements (Обустройство) в строку и обратно
	/// </summary>
	public class ArrangementsStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<Arrangements>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Тележки смотровые
		/// </summary>
		private static readonly string StrInspectionTrolleys = "Тележки смотровые";
		/// <summary>
		/// Люльки
		/// </summary>
		private static readonly string StrCradles = "Люльки";
		/// <summary>
		/// Смотровые хода
		/// </summary>
		private static readonly string StrObservationMoves = "Смотровые хода";
		/// <summary>
		/// Люки
		/// </summary>
		private static readonly string StrHatches = "Люки";
		/// <summary>
		/// Двери
		/// </summary>
		private static readonly string StrDoors = "Двери";
		/// <summary>
		/// Лестницы
		/// </summary>
		private static readonly string StrStairs = "Лестницы";
		private static readonly StringBuilder stringBuilder = new StringBuilder();

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public ArrangementsStrings() { }
		private static ArrangementsStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static ArrangementsStrings Instance
			=> instance ?? (instance = new ArrangementsStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления Arrangements (Обустройство)
		/// </summary>
		/// <param name="arrangements">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(Arrangements arrangements)
		{
			stringBuilder.Clear();
			bool first = true;
			if(arrangements == Arrangements.NoData)
			{
				return "Нет данных";
			}
			if ((arrangements & Arrangements.InspectionTrolleys) == Arrangements.InspectionTrolleys)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrInspectionTrolleys);
				first = false;
			}
			if ((arrangements & Arrangements.Cradles) == Arrangements.Cradles)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrCradles);
				first = false;
			}
			if ((arrangements & Arrangements.ObservationMoves) == Arrangements.ObservationMoves)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrObservationMoves);
				first = false;
			}
			if ((arrangements & Arrangements.Hatches) == Arrangements.Hatches)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrHatches);
				first = false;
			}
			if ((arrangements & Arrangements.Doors) == Arrangements.Doors)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrDoors);
				first = false;
			}
			if ((arrangements & Arrangements.Stairs) == Arrangements.Stairs)
			{
				if(!first) stringBuilder.Append(", ");
				stringBuilder.Append(StrStairs);
				first = false;
			}
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Метод для элемента перечисления Arrangements (Обустройство) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public Arrangements GetElement(string name)
		{
			var tmp = name.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			var res = Arrangements.NoData;
			foreach (var item in tmp)
			{
				if (item.Equals(StrInspectionTrolleys, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.InspectionTrolleys;
				else if (item.Equals(StrCradles, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.Cradles;
				else if (item.Equals(StrObservationMoves, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.ObservationMoves;
				else if (item.Equals(StrHatches, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.Hatches;
				else if (item.Equals(StrDoors, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.Doors;
				else if (item.Equals(StrStairs, StringComparison.OrdinalIgnoreCase))
					res |= Arrangements.Stairs;
			}
			return res;
		}
	}
}