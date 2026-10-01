using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoQuantity
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public string DefaultName { get; private set; }

		[JsonProperty]
		public string SiUnit { get; private set; }

		[JsonProperty]
		public List<DtoUnitInfo> Units { get; private set; }
	}
}
