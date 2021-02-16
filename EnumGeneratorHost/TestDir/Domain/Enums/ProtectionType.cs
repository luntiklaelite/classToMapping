using System;

namespace ITS.Core.Bridges.Domain.Enums
{
	/// <summary>
	/// Тип ограждения
	/// </summary>
	[Serializable]
	public enum ProtectionType : byte
	{
		/// <summary>
		/// Парапетное
		/// </summary>
		Parapet,
		/// <summary>
		/// Барьерное
		/// </summary>
		Barrier,
		/// <summary>
		/// Бордюрное
		/// </summary>
		Border,
		/// <summary>
		/// Тросовое
		/// </summary>
		Cable,
		/// <summary>
		/// Комбинированное
		/// </summary>
		Combined,
		/// <summary>
		/// Нет данных
		/// </summary>
		NoData,
	}
}