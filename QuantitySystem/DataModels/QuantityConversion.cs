using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class QuantityConversion
	{
		[JsonProperty]
		public string SourceQuantity { get; private set; }

		[JsonProperty]
		public string DestinationQuantity { get; private set; }

		[JsonProperty]
		public Calculation Conversion { get; private set; }

		public QuantityConversion(string sourceQuantity, string destinationQuantity, Calculation conversion)
		{
			SourceQuantity = sourceQuantity;
			DestinationQuantity = destinationQuantity;
			Conversion = conversion;
		}
	}
}
