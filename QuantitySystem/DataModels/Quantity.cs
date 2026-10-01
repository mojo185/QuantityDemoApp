using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class Quantity
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public string DefaultName { get; private set; }

		[JsonProperty]
		public string SiUnit { get; private set; }

		[JsonProperty]
		public List<UnitInfo> Units { get; private set; }

		public Quantity(string key, string defaultName, string siUnit, List<UnitInfo> units)
		{
			Key = key;
			DefaultName = defaultName;
			SiUnit = siUnit;
			Units = units;
		}
	}
}
