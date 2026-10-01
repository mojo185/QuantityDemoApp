using Newtonsoft.Json;
using System.Collections.Generic;

namespace QuantitySystem.DataModels.DtoDataModel
{
	public class DtoAddition
	{
		[JsonProperty]
		public List<DtoTerm> Terms { get; private set; }
	}
}
