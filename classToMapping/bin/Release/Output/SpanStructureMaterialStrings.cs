using ITS.Core.Bridges.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ITS.Core.Bridges.Domain.EnumString
{
	/// <summary>
	/// Класс-преобразователь перечисления SpanStructureMaterial в строку и обратно
	/// </summary>
	public class SpanStructureMaterialStrings : IEnumStrings<SpanStructureMaterial>
	{
		 public static Dictionary<SpanStructureMaterial, string> Strings =
			 new Dictionary<SpanStructureMaterial, string> {
				 { SpanStructureMaterial.ReinforcedConcretePrestressed,"Железобетон преднапряженный" },
				 { SpanStructureMaterial.Ferroconcrete,"Железобетон" },
				 { SpanStructureMaterial.Steel,"Сталь" },
				 { SpanStructureMaterial.SteelКeinforcedСoncrete,"Сталежелезобетон" },
				 { SpanStructureMaterial.Wood,"Древесина" },
				 { SpanStructureMaterial.GluedTimber,"Древесина клееная" },
				 { SpanStructureMaterial.StoneOrConcreteMasonry,"Каменная или бетонная кладка" },
				 { SpanStructureMaterial.Aluminum,"Алюминий" },
				 { SpanStructureMaterial.CompositeMaterial,"Композитный материал" },
			};
		public SpanStructureMaterialStrings() { }
		private static SpanStructureMaterialStrings instance;
		public static SpanStructureMaterialStrings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new BridgeStatusStrings();
				}
				return instance;
			}
		}

		public string GetName(SpanStructureMaterial span_structure_material)
		{
			return Strings[span_structure_material];
		}
		public SpanStructureMaterial GetName(string name)
		{
			return Strings.FirstOrDefault(s => s.Value == name).Key;
		}
	}
}
