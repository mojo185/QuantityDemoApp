using Newtonsoft.Json;
using QuantitySystem.DataModels.ReturnModel;
using QuantitySystem.Enumerations;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class SimpleCalculation
	{
		[JsonProperty]
		public double Multiplier { get; private set; } = 1.0d;

		[JsonProperty]
		public double Divisor { get; private set; } = 1.0d;

		[JsonProperty]
		public double Offset { get; private set; } = 0.0d;

		public SimpleCalculation(double multiplier, double divisor, double offset)
		{
			Multiplier = multiplier;
			Divisor = divisor;
			Offset = offset;
		}

		internal QuantityReturnModel CalculateToSi(double valueToConvert)
		{
			return new QuantityReturnModel() { Value = valueToConvert * Multiplier / Divisor + Offset, ErrorCode = (int)ErrorCodes.Success };
		}

		internal QuantityReturnModel CalculateFromSi(double valueToConvert)
		{
			return new QuantityReturnModel() { Value = (valueToConvert - Offset) * Divisor / Multiplier, ErrorCode = (int)ErrorCodes.Success };
		}
	}
}
