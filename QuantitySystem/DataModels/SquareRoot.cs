using Newtonsoft.Json;
using System;

namespace QuantitySystem.DataModels
{
	class SquareRoot : BaseOperation
	{
		const string OPERATOR = "sqrt";

		[JsonProperty]
		public Term SqrtTerm { get; private set; }

		internal SquareRoot(Term sqrtTerm)
		{
			SqrtTerm = sqrtTerm;
		}

		internal override double Calculate(double valueToConvert)
		{
			return Math.Sqrt(SqrtTerm.GetValue(valueToConvert));
		}

		internal override string GenerateCalculationRepresentation()
		{
			return $"{BEGIN_BRACKET}{OPERATOR}{BEGIN_BRACKET}{SqrtTerm.GenerateCalculationRepresentation()}{END_BRACKET}{END_BRACKET}";
		}
	}
}
