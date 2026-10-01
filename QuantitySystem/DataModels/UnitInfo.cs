using Newtonsoft.Json;
using QuantitySystem.DataModels.ReturnModel;
using QuantitySystem.Enumerations;
using QuantitySystem.Texts;
using System.Collections.Generic;

namespace QuantitySystem.DataModels
{
	public class UnitInfo
	{
		[JsonProperty]
		public string Unit { get; private set; }

		[JsonProperty]
		public SimpleCalculation SimpleCalc { get; private set; }

		[JsonProperty]
		public Calculations ConversionCalculation { get; private set; }

		[JsonProperty]
		public List<ReferenceCondition> ReferenceConditions { get; private set; }

		public UnitInfo(string unit, SimpleCalculation simpleCalc, Calculations conversionCalculation, List<ReferenceCondition> referenceConditions)
		{
			Unit = unit;
			SimpleCalc = simpleCalc;
			ConversionCalculation = conversionCalculation;
			ReferenceConditions = referenceConditions;
		}

		internal QuantityReturnModel CalculationToSi(double valueToConvert)
		{
			if (ConversionCalculation != null)
			{
				return new QuantityReturnModel()
				{
					Value = ConversionCalculation.CalculationToSi.Calculate(valueToConvert),
					ErrorCode = (int) ErrorCodes.Success
				}; 
			}
			else if (SimpleCalc != null)
			{
				return SimpleCalc.CalculateToSi(valueToConvert);
			}

			return new QuantityReturnModel() {ErrorCode = (int) ErrorCodes.NoConversionCalculationAvailable,
											  ErrorMessage = ErrorMessages.NoConversionCalculationAvailable};
		}

		internal QuantityReturnModel CalculationFromSi(double valueToConvert)
		{
			if (ConversionCalculation != null)
			{
				return new QuantityReturnModel()
				{
					Value = ConversionCalculation.CalculationFromSi.Calculate(valueToConvert),
					ErrorCode = (int)ErrorCodes.Success
				}; 
			}
			else if (SimpleCalc != null)
			{
				return SimpleCalc.CalculateFromSi(valueToConvert);
			}

			return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.NoConversionCalculationAvailable,
											   ErrorMessage = ErrorMessages.NoConversionCalculationAvailable};
		}
	}
}
