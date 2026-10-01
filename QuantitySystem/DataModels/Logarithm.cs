using Newtonsoft.Json;
using System;

namespace QuantitySystem.DataModels
{
	class Logarithm : BaseOperation
	{
		const string OPERATOR = "log";

		[JsonProperty]
		public Term LogTerm { get; private set; }

		internal Logarithm(Term logTerm)
		{
			LogTerm = logTerm;
		}

		internal override double Calculate(double valueToConvert)
		{
			return Math.Log(LogTerm.GetValue(valueToConvert));
		}

		internal override string GenerateCalculationRepresentation()
		{
			return $"{BEGIN_BRACKET}{OPERATOR}{BEGIN_BRACKET}{LogTerm.GenerateCalculationRepresentation()}{END_BRACKET}{END_BRACKET}";
		}
	}
}
