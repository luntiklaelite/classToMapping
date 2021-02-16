using ITS.Core.Bridges.Domain.Enums;
using System;

namespace ITS.Core.Bridges.Domain.EnumStrings
{
	/// <summary>
	/// Класс-преобразователь перечисления DrainageType (Тип водоотвода) в строку и обратно
	/// </summary>
	public class DrainageTypeStrings : ITS.Core.Bridges.Domain.Base.IEnumStrings<DrainageType>, ITS.Core.Bridges.Domain.Base.EnumStrings
	{
		/// <summary>
		/// Водоотвод не организован
		/// </summary>
		private static readonly string StrNone = "Водоотвод не организован";
		/// <summary>
		/// Через водоотводные трубки со сбросом под мостовое сооружение
		/// </summary>
		private static readonly string StrDrainPipesDischargeUnderBridge = "Через водоотводные трубки со сбросом под мостовое сооружение";
		/// <summary>
		/// Через водоотводные трубки с отводом воды по водопроводу (лотку, трубе) вдоль мостового сооружения
		/// </summary>
		private static readonly string StrDrainPipesWaterSupply = "Через водоотводные трубки с отводом воды по водопроводу (лотку, трубе) вдоль мостового сооружения";
		/// <summary>
		/// Сток воды вдоль проезжей части за счет уклонов за пределы мостового сооружения
		/// </summary>
		private static readonly string StrWaterSlopes = "Сток воды вдоль проезжей части за счет уклонов за пределы мостового сооружения";
		/// <summary>
		/// Сброс воды поперек мостового сооружения через тротуары
		/// </summary>
		private static readonly string StrWaterDischargeSidewalks = "Сброс воды поперек мостового сооружения через тротуары";
		/// <summary>
		/// По лоткам (продольным или поперечным) за пределы мостового сооружения
		/// </summary>
		private static readonly string StrByTrays = "По лоткам (продольным или поперечным) за пределы мостового сооружения";
		/// <summary>
		/// Через зазоры в проезжей части мостового сооружения (дощатый настил и т. д.)
		/// </summary>
		private static readonly string StrTheGapsCarriageway = "Через зазоры в проезжей части мостового сооружения (дощатый настил и т. д.)";
		/// <summary>
		/// Комбинированный (например, за счет уклонов и лотков)
		/// </summary>
		private static readonly string StrCombined = "Комбинированный (например, за счет уклонов и лотков)";
		/// <summary>
		/// Нет данных
		/// </summary>
		private static readonly string StrNoData = "Нет данных";

		/// <summary>
		/// Конструктор по-умолчанию
		/// </summary>
		public DrainageTypeStrings() { }
		private static DrainageTypeStrings instance;
		/// <summary>
		/// Свойство для обращения к методам без создания нового экземпляра этого класса
		/// </summary>
		public static DrainageTypeStrings Instance
			=> instance ?? (instance = new DrainageTypeStrings());

		/// <summary>
		/// Метод для получения строкового представления элемента перечисления DrainageType (Тип водоотвода)
		/// </summary>
		/// <param name="enumElement">Элемент перечисления</param>
		/// <returns>Строка-результат преобразования</returns>
		public string GetName(DrainageType enumElement)
		{
			switch(enumElement)
			{
				case DrainageType.None:
					return StrNone;
				case DrainageType.DrainPipesDischargeUnderBridge:
					return StrDrainPipesDischargeUnderBridge;
				case DrainageType.DrainPipesWaterSupply:
					return StrDrainPipesWaterSupply;
				case DrainageType.WaterSlopes:
					return StrWaterSlopes;
				case DrainageType.WaterDischargeSidewalks:
					return StrWaterDischargeSidewalks;
				case DrainageType.ByTrays:
					return StrByTrays;
				case DrainageType.TheGapsCarriageway:
					return StrTheGapsCarriageway;
				case DrainageType.Combined:
					return StrCombined;
				case DrainageType.NoData:
					return StrNoData;
			}
			throw new ArgumentException("Некорректный элемент перечисления", "enumElement");
		}

		/// <summary>
		/// Метод для элемента перечисления DrainageType (Тип водоотвода) из строкового представления
		/// </summary>
		/// <param name="name">Строковое представление элемента перечисления</param>
		/// <returns>Соответствующий элемент перечисления</returns>
		public DrainageType GetElement(string name)
		{
			if (name == StrNone)
				return DrainageType.None;
			if (name == StrDrainPipesDischargeUnderBridge)
				return DrainageType.DrainPipesDischargeUnderBridge;
			if (name == StrDrainPipesWaterSupply)
				return DrainageType.DrainPipesWaterSupply;
			if (name == StrWaterSlopes)
				return DrainageType.WaterSlopes;
			if (name == StrWaterDischargeSidewalks)
				return DrainageType.WaterDischargeSidewalks;
			if (name == StrByTrays)
				return DrainageType.ByTrays;
			if (name == StrTheGapsCarriageway)
				return DrainageType.TheGapsCarriageway;
			if (name == StrCombined)
				return DrainageType.Combined;
			if (name == StrNoData)
				return DrainageType.NoData;
			throw new ArgumentException("Некорректная входная строка", "name");
		}
	}
}