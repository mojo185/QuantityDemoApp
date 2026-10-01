using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoCalculation
	{
		[JsonProperty]
		public DtoAddition Addition { get; private set; }

		[JsonProperty]
		public DtoMultiplication Multiplication { get; private set; }

		[JsonProperty]
		public DtoSubtraction Subtraction { get; private set; }

		[JsonProperty]
		public DtoDivision Division { get; private set; }

		[JsonProperty]
		public DtoExponential Exponential { get; private set; }

		[JsonProperty]
		public DtoSquareRoot SquareRoot { get; private set; }

		[JsonProperty]
		public DtoLogarithm Logarithm { get; private set; }
	}
}
