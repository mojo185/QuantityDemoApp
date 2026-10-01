using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoTerm
	{
		[JsonProperty]
		public double Value { get; private set; } = 0.0;

		[JsonProperty]
		public string Constant { get; private set; }

		/// <summary>
		/// Indicates the value which is given and needs to be used in the calculation.
		/// </summary>
		[JsonProperty]
		public bool KnownVariable { get; private set; } = false;

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
