using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип водоотвода
	/// </summary>
	[Serializable]
	public enum DrainageType : byte
	{
		/// <summary>
		/// Водоотвод не организован
		/// </summary>
		None,
		/// <summary>
		/// Через водоотводные трубки со сбросом под мостовое сооружение
		/// </summary>
		DrainPipesDischargeUnderBridge,
		/// <summary>
		/// Через водоотводные трубки с отводом воды по водопроводу (лотку, трубе) вдоль мостового сооружения
		/// </summary>
		DrainPipesWaterSupply,
		/// <summary>
		/// Сток воды вдоль проезжей части за счет уклонов за пределы мостового сооружения
		/// </summary>
		WaterSlopes,
		/// <summary>
		/// Сброс воды поперек мостового сооружения через тротуары
		/// </summary>
		WaterDischargeSidewalks,
		/// <summary>
		/// По лоткам (продольным или поперечным) за пределы мостового сооружения
		/// </summary>
		ByTrays,
		/// <summary>
		/// Через зазоры в проезжей части мостового сооружения (дощатый настил и т. д.)
		/// </summary>
		TheGapsCarriageway,
		/// <summary>
		/// Комбинированный (например, за счет уклонов и лотков)
		/// </summary>
		Combined,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}