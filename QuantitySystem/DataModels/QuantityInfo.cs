using Newtonsoft.Json;

namespace QuantitySystem.DataModels
{
	public class QuantityInfo
	{
		[JsonProperty]
		public Version Version { get; private set; }

		public QuantityInfo(Version version)
		{
			Version = version;
		}
	}
}