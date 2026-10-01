using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoQuantityInfo
	{
		[JsonProperty]
		public Version Version { get; private set; }
	}
}
