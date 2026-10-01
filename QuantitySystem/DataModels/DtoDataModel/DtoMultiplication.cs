using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoMultiplication
	{
		[JsonProperty]
		public List<DtoTerm> Terms { get; private set; }
	}
}
