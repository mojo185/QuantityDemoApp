using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class Calculations
	{
		[JsonProperty]
		public Calculation CalculationToSi { get; private set; }

		[JsonProperty]
		public Calculation CalculationFromSi { get; private set; }

		public Calculations(Calculation calculationToSi, Calculation calculationFromSi)
		{
			CalculationToSi = calculationToSi;
			CalculationFromSi = calculationFromSi;
		}
	}
}
