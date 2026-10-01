using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoQuantityData
	{
		[JsonProperty]
		public DtoQuantityInfo QuantitySystemInfo { get; private set; }

		[JsonProperty]
		public List<DtoQuantityConstant> Constants { get; private set; }

		[JsonProperty]
		public List<DtoReferenceCondition> StandardReferenceConditions { get; private set; }

		[JsonProperty]
		public List<DtoQuantity> Quantities { get; private set; }

		[JsonProperty]
		public List<DtoQuantityConversion> QuantityConversions { get; private set; }
	}
}
