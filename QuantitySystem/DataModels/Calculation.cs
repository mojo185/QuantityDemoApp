using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class Calculation
	{
		[JsonProperty]
		public BaseOperation Operation { get; protected set; }

		public Calculation(BaseOperation operation)
		{
			Operation = operation;
		}

		public double Calculate(double valueToConvert)
		{
			return Operation.Calculate(valueToConvert);
		}

		public virtual string GenerateCalculationRepresentation()
		{
			return Operation.GenerateCalculationRepresentation();
		}
	}
}
