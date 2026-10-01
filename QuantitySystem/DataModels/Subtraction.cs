using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class Subtraction : BaseOperation
	{
		const string OPERATOR = "-";

		[JsonProperty]
		public Term Minuend { get; private set; }

		[JsonProperty]
		public List<Term> Subtrahend { get; private set; }

		internal Subtraction(Term minuend, List<Term> subtrahend)
		{
			Minuend = minuend;
			Subtrahend = subtrahend;
		}

		internal override double Calculate(double valueToConvert)
		{
			double result = Minuend != null ? Minuend.GetValue(valueToConvert) : 0.0;

			if (Subtrahend != null)
			{
				foreach (Term term in Subtrahend)
				{
					result -= term.GetValue(valueToConvert);
				}
			}

			return result;
		}

		internal override string GenerateCalculationRepresentation()
		{
			string result = $"{BEGIN_BRACKET}{Minuend.GenerateCalculationRepresentation()}";

			if (Subtrahend != null)
			{
				bool first = true;

				foreach (Term term in Subtrahend)
				{
					result += first ? term.GenerateCalculationRepresentation() : $" {OPERATOR} {term.GenerateCalculationRepresentation()}";
					first = false;
				}
			}

			return result + END_BRACKET;
		}
	}
}
