using Newtonsoft.Json;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoDivision
	{
		[JsonProperty]
		public DtoTerm Dividend { get; private set; }

		[JsonProperty]
		public DtoTerm Divisor { get; private set; }
	}
}
