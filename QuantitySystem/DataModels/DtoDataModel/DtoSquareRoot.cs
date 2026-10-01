using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoSquareRoot
	{
		[JsonProperty]
		public DtoTerm SqrtTerm { get; private set; }
	}
}
