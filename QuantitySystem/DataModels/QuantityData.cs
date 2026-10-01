using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class QuantityData
	{
		[JsonProperty]
		public QuantityInfo QuantitySystemInfo { get; private set; }

		[JsonProperty]
		public List<QuantityConstant> Constants { get; private set; }

		[JsonProperty]
		public List<ReferenceCondition> StandardReferenceConditions { get; private set; }

		[JsonProperty]
		public List<Quantity> Quantities { get; private set; }

		[JsonProperty]
		public List<QuantityConversion> QuantityConversions { get; private set; }

		public QuantityData(QuantityInfo quantitySystemInfo, List<QuantityConstant> constants, List<ReferenceCondition> standardReferenceConditions, List<Quantity> quantities,
							List<QuantityConversion> quantityConversions)
		{
			QuantitySystemInfo = quantitySystemInfo;
			Constants = constants;
			StandardReferenceConditions = standardReferenceConditions;
			Quantities = quantities;
			QuantityConversions = quantityConversions;
		}
	}
}
