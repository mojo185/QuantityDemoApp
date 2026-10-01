using Newtonsoft.Json;
namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoReferenceCondition
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public string DefaultName { get; private set; }

		[JsonProperty]
		public double Value { get; private set; }
	}
}
