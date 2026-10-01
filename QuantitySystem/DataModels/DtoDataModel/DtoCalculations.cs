using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoCalculations
	{
		[JsonProperty]
		public DtoCalculation CalculationToSi { get; private set; }

		[JsonProperty]
		public DtoCalculation CalculationFromSi { get; private set; }
	}
}
