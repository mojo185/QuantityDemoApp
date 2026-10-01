using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoExponential
	{
		[JsonProperty]
		public DtoTerm PowerTerm { get; private set; }
	}
}
