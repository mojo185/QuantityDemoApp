
using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class QuantityConstant
	{
		[JsonProperty]
		public string Key { get; private set; }

		[JsonProperty]
		public double Value { get; private set; }

		public QuantityConstant(string key, double value)
		{
			Key = key;
			Value = value;
		}
	}
}
