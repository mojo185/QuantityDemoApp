using Newtonsoft.Json;
using System;

namespace QuantitySystem.DataModels
{
	public class Term : Calculation
	{
		[JsonProperty]
		public double Value { get; private set; } = 0.0;

		/// <summary>
		/// Indicates the value which is given and needs to be used in the calculation.
		/// </summary>
		[JsonProperty]
		public bool KnownVariable { get; private set; } = false;

		public Term(double value, bool knownVariable, BaseOperation operation) : base(operation)
		{
			Value = value;
			KnownVariable = knownVariable;
		}

		public Term(double value) : base(null)
		{
			Value = value;
		}

		public Term(bool knownVariable) : base(null)
		{
			KnownVariable = knownVariable;
		}

		public Term(BaseOperation operation) : base(operation)
		{

		}

		public double GetValue(double valueToConvert)
		{
			double result = 0.0;

			if (Operation != null)
			{
				result = Operation.Calculate(valueToConvert);
			}
			else if (KnownVariable)
			{
				result = valueToConvert;
			}
			else
			{
				result = Value;
			}

			return result;
		}

		public override string GenerateCalculationRepresentation()
		{
			if (KnownVariable)
			{
				return "x";
			}
			else if (Operation != null)
			{
				return Operation.GenerateCalculationRepresentation();
			}
			else
			{
				return Value.ToString();
			}
		}
	}
}
