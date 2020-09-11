//Generated by EnumGenerator, 02.09.2020 13:21:23
using System;
using System.Text;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Класс-преобразователь перечисления Arrangements в строку и обратно
	/// </summary>
	public class ArrangementsStrings : IEnumStrings<Arrangements>
	{
		private static readonly string StrInspectionTrolleys = "Тележки смотровые";
		private static readonly string StrCradles = "Люльки";
		private static readonly string StrObservationMoves = "Смотровые хода";
		private static readonly string StrHatches = "Люки";
		private static readonly string StrDoors = "Двери";
		private static readonly string StrStairs = "Лестницы";
		private static readonly StringBuilder stringBuilder = new StringBuilder();

		public ArrangementsStrings() { }
		private static ArrangementsStrings instance;
		public static ArrangementsStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ArrangementsStrings();
				}
				return instance;
			}
		}

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

		public Arrangements GetElement(string name)
		{
			var tmp = name.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			var res = Arrangements.NoData;
			foreach (var item in tmp)
			{
				if (item == StrInspectionTrolleys)
					res |= Arrangements.InspectionTrolleys;
				else if (item == StrCradles)
					res |= Arrangements.Cradles;
				else if (item == StrObservationMoves)
					res |= Arrangements.ObservationMoves;
				else if (item == StrHatches)
					res |= Arrangements.Hatches;
				else if (item == StrDoors)
					res |= Arrangements.Doors;
				else if (item == StrStairs)
					res |= Arrangements.Stairs;
			}
			return res;
		}
	}
}
