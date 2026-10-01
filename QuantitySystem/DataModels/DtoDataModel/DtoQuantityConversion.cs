
using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoQuantityConversion
	{
		[JsonProperty]
		public string SourceQuantity { get; private set; }

		[JsonProperty]
		public string DestinationQuantity { get; private set; }

		[JsonProperty]
		public DtoCalculation Conversion { get; private set; }
	}
}
