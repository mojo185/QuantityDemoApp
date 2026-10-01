using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoSubtraction
	{
		[JsonProperty]
		public DtoTerm Minuend { get; private set; }

		[JsonProperty]
		public List<DtoTerm> Subtrahend { get; private set; }
	}
}
