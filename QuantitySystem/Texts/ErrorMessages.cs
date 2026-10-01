using System;

namespace QuantitySystem.Texts
{
	internal static class ErrorMessages
	{
		internal static string Success = String.Empty;

		internal static string UnknownError = "An unknown error has occurred.";

		internal static string NoConversionCalculationAvailable = "There is no conversion calculation available.";

		internal static string DivisionByZeroError = "A division by zero occurred, check all inputs for zero.";

		internal static string QuantityNotGiven = "No quantity given to convert.";

		internal static string ToUnitNotGiven = "No unit given to convert to.";

		internal static string FromUnitNotGiven = "No unit given to convert from.";

		internal static string ToUnitNotFound = "The requested unit to convert to was not found in the quantity system.";

		internal static string FromUnitNotFound = "The requested unit to convert from was not found in the quantity system.";

		internal static string SiUnitNotAvailable = "SI unit not available.";
	}
}
