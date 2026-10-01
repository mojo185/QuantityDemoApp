using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using QuantitySystem.DataModels;
using QuantitySystem.DataModels.DtoDataModel;
using QuantitySystem.DataModels.ReturnModel;
using QuantitySystem.QuantityProcessing;
using QuantitySystem.Enumerations;
using QuantitySystem.Texts;
using System.IO;

namespace QuantitySystem
{
	public static class QuantitySystemCore
	{
		static QuantityData QuantitySystemData { get; set; }

		public static void LoadQuantitySystem(string fileName)
		{
			QuantitySystemData = DtoQuantityConverter.ConvertDtoQuantityData(LoadQuantityData(fileName));
		}

		public static bool IsNewerVersion(string quantitiesFile, string quantitiesFileBase)
		{
			return LoadQuantityData(quantitiesFile).QuantitySystemInfo.Version.CompareTo(LoadQuantityData(quantitiesFileBase).QuantitySystemInfo.Version) > 0;
		}

		private static DtoQuantityData LoadQuantityData(string fileName)
		{
			string json = File.ReadAllText(fileName);

			return JsonConvert.DeserializeObject<DtoQuantityData>(json);
		}

		public static Quantity GetQuantity(string quantityKey)
		{
			if (String.IsNullOrEmpty(quantityKey))
			{
				// TODO: log error no quantity key given.
				return null;
			}

			if (QuantitySystemData == null)
			{
				throw new QuantitySystemNotInitializedException();
			}

			Quantity result = QuantitySystemData.Quantities.Find(q => String.Equals(q.Key, quantityKey));

			if (result == null)
			{
				// TODO: Log error no quantity found for the given key.
			}

			return result;
		}

		public static List<string> GetQuantityUnits(string quantityKey)
		{
			if (QuantitySystemData == null)
			{
				throw new QuantitySystemNotInitializedException();
			}

			List<string> result = new List<string>();

			Quantity quantity = QuantitySystemData.Quantities.Find(q => String.Equals(q.Key, quantityKey));

			if (quantity == null)
			{
				// TODO: Log error no quantity found for the given key.
			}
			else
			{
				if (quantity.Units == null)
				{
					// TODO: Log error no units available for this quantity.
				}
				else
				{
					foreach (UnitInfo unit in quantity.Units)
					{
						result.Add(unit.Unit);
					}
				}
			}

			return result;
		}

		public static Dictionary<string, string> GetAllQuantities()
		{
			if (QuantitySystemData == null)
			{
				throw new QuantitySystemNotInitializedException();
			}

			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (Quantity quantity in QuantitySystemData.Quantities)
			{
				result.Add(quantity.Key, quantity.DefaultName);
			}

			return result;
        }

        public static QuantityReturnModel ConvertToSi(Quantity quantity, string fromUnitKey, double valueToConvert)
		{
			// Why check null and not null for the fromUnitKey and the toUnitKey or empty? It can be that the unit is nonexistent. Like in fraction.
			// The siUnit is a decimal like 0.5, another unit of this quantity is percentage which gives us 50%.
			if (quantity == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.QuantityNotGiven, ErrorMessage = ErrorMessages.QuantityNotGiven };
			}
			else if (fromUnitKey == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.FromUnitNotGiven, ErrorMessage = ErrorMessages.FromUnitNotGiven };
			}
			else if (quantity.SiUnit == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.SiUnitNotAvailable, ErrorMessage = ErrorMessages.SiUnitNotAvailable };
			}

			return ConvertToUnit(quantity, fromUnitKey, quantity.SiUnit, valueToConvert);
		}

		public static QuantityReturnModel ConvertToUnit(Quantity quantity, string fromUnitKey, string toUnitKey, double valueToConvert)
		{
			// Why check null and not null for the fromUnitKey and the toUnitKey or empty? It can be that the unit is nonexistent. Like in fraction.
			// The siUnit is a decimal like 0.5, another unit of this quantity is percentage which gives us 50%.
			if (quantity == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.QuantityNotGiven, ErrorMessage = ErrorMessages.QuantityNotGiven };
			}
			else if (fromUnitKey == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.FromUnitNotGiven, ErrorMessage = ErrorMessages.FromUnitNotGiven };
			}
			else if (toUnitKey == null)
			{
				// TODO: Log error
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.ToUnitNotGiven, ErrorMessage = ErrorMessages.ToUnitNotGiven };
			}

			// Check if to and from unit is the same.
			if (String.Equals(fromUnitKey, toUnitKey))
			{
				return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.Success, Value = valueToConvert };
			}

			// Check if from unit is equal to SI.
			if (String.Equals(fromUnitKey, quantity.SiUnit))
			{
				UnitInfo toUnit = quantity.Units.Find(u => String.Equals(u.Unit, toUnitKey));

				if (toUnit == null)
				{
					// TODO: Log error
					return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.ToUnitNotFound, ErrorMessage = ErrorMessages.ToUnitNotFound };
				}
				else
				{
					return toUnit.CalculationFromSi(valueToConvert);
				}
			}
			else if (String.Equals(toUnitKey, quantity.SiUnit))
			{
				UnitInfo fromUnit = quantity.Units.Find(u => String.Equals(u.Unit, fromUnitKey));

				if (fromUnit == null)
				{
					// TODO: Log error
					return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.FromUnitNotFound, ErrorMessage = ErrorMessages.FromUnitNotFound };
				}
				else
				{
					return fromUnit.CalculationToSi(valueToConvert);
				}
			}
			else
			{
				UnitInfo fromUnit = quantity.Units.Find(u => String.Equals(u.Unit, fromUnitKey));
				UnitInfo toUnit = quantity.Units.Find(u => String.Equals(u.Unit, toUnitKey));

				if (fromUnit == null)
				{
					// TODO: Log error
					return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.FromUnitNotFound, ErrorMessage = ErrorMessages.FromUnitNotFound };
				}
				else if (toUnit == null)
				{
					// TODO: Log error
					return new QuantityReturnModel() { ErrorCode = (int)ErrorCodes.ToUnitNotFound, ErrorMessage = ErrorMessages.ToUnitNotFound };
				}
				else
				{
					QuantityReturnModel siResult = fromUnit.CalculationToSi(valueToConvert);

					if (siResult != null && siResult.ErrorCode == (int)ErrorCodes.Success)
					{
						return toUnit.CalculationFromSi(siResult.Value);
					}

					return siResult;
				}
			}
		}
	}
}
