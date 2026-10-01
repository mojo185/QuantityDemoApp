using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoQuantityConstant
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public double Value { get; private set; }
	}
}
