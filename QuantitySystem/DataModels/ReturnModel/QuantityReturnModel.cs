using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitySystem.DataModels.ReturnModel
{
	public class QuantityReturnModel
	{
		public double Value { get; set; }

		public int ErrorCode { get; set; }

		public string ErrorMessage { get; set; }
	}
}
