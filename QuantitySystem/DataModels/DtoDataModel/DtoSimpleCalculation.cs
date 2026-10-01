using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoSimpleCalculation
	{
		[JsonProperty]
		public double Multiplier { get; private set; }

		[JsonProperty]
		public double Divisor { get; private set; }

		[JsonProperty]
		public double Offset { get; private set; }
	}
}
