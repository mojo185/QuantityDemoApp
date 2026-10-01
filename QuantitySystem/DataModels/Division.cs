using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class Division : BaseOperation
	{
		const string OPERATOR = "/";

		[JsonProperty]
		public Term Dividend { get; private set; }

		[JsonProperty]
		public Term Divisor { get; private set; }

		internal Division(Term dividend, Term divisor)
		{
			Dividend = dividend;
			Divisor = divisor;
		}

		internal override double Calculate(double valueToConvert)
		{
			return Dividend.GetValue(valueToConvert) / Divisor.GetValue(valueToConvert);
		}

		internal override string GenerateCalculationRepresentation()
		{
			return $"{BEGIN_BRACKET}{Dividend.GenerateCalculationRepresentation()} {OPERATOR} {Divisor.GenerateCalculationRepresentation()}{END_BRACKET}";
		}
	}
}
