using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class Addition : BaseOperation
	{
		const string OPERATOR = "+";

		[JsonProperty]
		public List<Term> Terms { get; private set; }

		internal Addition(List<Term> terms)
		{
			Terms = terms;
		}

		internal override double Calculate(double valueToConvert)
		{
			double result = 0.0;

			if (Terms != null)
			{
				foreach (Term term in Terms)
				{
					result += term.GetValue(valueToConvert);
				}
			}

			return result;
		}

		internal override string GenerateCalculationRepresentation()
		{
			string result = BEGIN_BRACKET;

			if (Terms != null)
			{
				bool first = true;

				foreach (Term term in Terms)
				{
					result += first ? term.GenerateCalculationRepresentation() : $" {OPERATOR} {term.GenerateCalculationRepresentation()}";
					first = false;
				}
			}

			return result + END_BRACKET;
		}
	}
}
