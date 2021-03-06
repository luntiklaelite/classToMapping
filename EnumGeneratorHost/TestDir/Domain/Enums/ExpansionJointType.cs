using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Типы деформационных швов 
	/// </summary>
	[Serializable]
	public enum ExpansionJointType : byte
	{
		/// <summary>
		/// Открытый
		/// </summary>
		Opened,
		/// <summary>
		/// Закрытый
		/// </summary>
		Closed,
		/// <summary>
		/// Заполненный
		/// </summary>
		Filled,
		/// <summary>
		/// Перекрытый
		/// </summary>
		Overlapped,
		/// <summary>
		/// Откатный
		/// </summary>
		Retractable,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}