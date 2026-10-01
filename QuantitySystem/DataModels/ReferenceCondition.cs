using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class ReferenceCondition
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public string DefaultName { get; private set; }

		[JsonProperty]
		public double Value { get; private set; }

		public ReferenceCondition(string key, string defaultName, double value)
		{
			Key = key;
			DefaultName = defaultName;
			Value = value;
		}
	}
}
