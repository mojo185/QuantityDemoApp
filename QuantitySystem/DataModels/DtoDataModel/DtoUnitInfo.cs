using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoUnitInfo
	{
		[JsonProperty]
		public string Unit { get; private set; }

		[JsonProperty]
		public DtoSimpleCalculation SimpleCalc { get; private set; }

		[JsonProperty]
		public DtoCalculations ConversionCalculation { get; private set; }

		[JsonProperty]
		public List<DtoReferenceCondition> ReferenceConditions { get; private set; }
	}
}
