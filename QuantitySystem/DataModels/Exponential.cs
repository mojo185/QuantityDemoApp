using Newtonsoft.Json;
using System;

namespace QuantitySystem.DataModels
{
	public class Exponential : BaseOperation
	{
		const string OPERATOR = "e^";

		[JsonProperty]
		public Term PowerTerm { get; private set; }

		internal Exponential(Term powerTerm)
		{
			PowerTerm = powerTerm;
		}

		internal override double Calculate(double valueToConvert)
		{
			return Math.Exp(PowerTerm.GetValue(valueToConvert));
		}

		internal override string GenerateCalculationRepresentation()
		{
			return $"{BEGIN_BRACKET}{OPERATOR}{BEGIN_BRACKET}{PowerTerm.GenerateCalculationRepresentation()}{END_BRACKET}{END_BRACKET}";
		}
	}
}
