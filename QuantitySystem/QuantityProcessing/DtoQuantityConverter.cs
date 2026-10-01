using QuantitySystem.DataModels;
using QuantitySystem.DataModels.DtoDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitySystem.QuantityProcessing
{
	internal static class DtoQuantityConverter
	{
		public static QuantityData ConvertDtoQuantityData(DtoQuantityData dtoQuantityData)
		{
			if (dtoQuantityData == null)
			{
				return null;
			}

			QuantityData quantityData = new QuantityData(
				ConvertSystemInfo(dtoQuantityData.QuantitySystemInfo),
				ConvertConstants(dtoQuantityData.Constants),
				ConvertDtoReferenceConditions(dtoQuantityData.StandardReferenceConditions),
				ConvertDtoQuantities(dtoQuantityData.Quantities),
				ConvertDtoQuantityConversions(dtoQuantityData.QuantityConversions)
				);

			return quantityData;
		}

		static QuantityInfo ConvertSystemInfo(DtoQuantityInfo dtoQuantityInfo)
		{
			return dtoQuantityInfo == null ? null : new QuantityInfo(dtoQuantityInfo.Version);
		}

		static List<QuantityConstant> ConvertConstants(List<DtoQuantityConstant> dtoQuantityConstants)
		{
			if (dtoQuantityConstants == null)
			{
				return null;
			}

			List<QuantityConstant> quantityConstants = new List<QuantityConstant>();

			foreach (DtoQuantityConstant dtoQuantityConstant in dtoQuantityConstants)
			{
				QuantityConstant quantityConstant = ConvertDtoConstant(dtoQuantityConstant);

				if (quantityConstant != null)
				{
					quantityConstants.Add(quantityConstant);
				}
			}

			return quantityConstants;
		}

		static QuantityConstant ConvertDtoConstant(DtoQuantityConstant dtoQuantityConstant)
		{
			return dtoQuantityConstant == null ? null : new QuantityConstant(dtoQuantityConstant.Key, dtoQuantityConstant.Value);
		}

		static List<Quantity> ConvertDtoQuantities(List<DtoQuantity> dtoQuantities)
		{
			if (dtoQuantities == null)
			{
				return null;
			}

			List<Quantity> quantities = new List<Quantity>();

			foreach (DtoQuantity dtoQuantity in dtoQuantities)
			{
				Quantity quantity = ConvertDtoQuantity(dtoQuantity);

				if (quantity != null)
				{
					quantities.Add(quantity);
				}
			}

			return quantities;
		}

		static Quantity ConvertDtoQuantity(DtoQuantity dtoQuantity)
		{
			return dtoQuantity == null ? null : new Quantity(dtoQuantity.Key, dtoQuantity.DefaultName, dtoQuantity.SiUnit, ConvertDtoUnits(dtoQuantity.Units));
		}

		static List<UnitInfo> ConvertDtoUnits(List<DtoUnitInfo> dtoUnits)
		{
			if (dtoUnits == null)
			{
				return null;
			}

			List<UnitInfo> units = new List<UnitInfo>();

			foreach (DtoUnitInfo dtoUnit in dtoUnits)
			{
				UnitInfo unit = ConvertDtoUnit(dtoUnit);

				if (unit != null)
				{
					units.Add(unit);
				}
			}

			return units;
		}

		static UnitInfo ConvertDtoUnit(DtoUnitInfo dtoUnit)
		{
			return dtoUnit == null ? null : new UnitInfo(dtoUnit.Unit, ConvertDtoSimpleCalculation(dtoUnit.SimpleCalc), 
														 ConvertDtoConversionCalculation(dtoUnit.ConversionCalculation),
														 ConvertDtoReferenceConditions(dtoUnit.ReferenceConditions));
		}

		static SimpleCalculation ConvertDtoSimpleCalculation(DtoSimpleCalculation dtoSimpleCalc)
		{
			return dtoSimpleCalc == null ? null : new SimpleCalculation(dtoSimpleCalc.Multiplier, dtoSimpleCalc.Divisor, dtoSimpleCalc.Offset);
		}

		static List<ReferenceCondition> ConvertDtoReferenceConditions(List<DtoReferenceCondition> dtoReferenceConditions)
		{
			if (dtoReferenceConditions == null)
			{
				return null;
			}

			List<ReferenceCondition> referenceConditions = new List<ReferenceCondition>();

			foreach (DtoReferenceCondition dtoReferenceCondition in dtoReferenceConditions)
			{
				ReferenceCondition referenceCondition = ConvertDtoReferenceCondition(dtoReferenceCondition);

				if (referenceCondition != null)
				{
					referenceConditions.Add(referenceCondition);
				}
			}

			return referenceConditions;
		}

		static ReferenceCondition ConvertDtoReferenceCondition(DtoReferenceCondition dtoReferenceCondition)
		{
			return dtoReferenceCondition == null ? null : new ReferenceCondition(dtoReferenceCondition.Key, dtoReferenceCondition.DefaultName,
																				 dtoReferenceCondition.Value);
		}

		static Calculations ConvertDtoConversionCalculation(DtoCalculations dtoConversionCalculation)
		{
			return dtoConversionCalculation == null ? null :
				new Calculations(ConvertDtoCalculation(dtoConversionCalculation.CalculationToSi),
								 ConvertDtoCalculation(dtoConversionCalculation.CalculationFromSi));
		}

		static Calculation ConvertDtoCalculation(DtoCalculation dtoCalculation)
		{
			return dtoCalculation == null ? null : new Calculation(ConvertDtoOperation(dtoCalculation));
		}

		static BaseOperation ConvertDtoOperation(DtoCalculation dtoCalculation)
		{
			if (dtoCalculation == null)
			{
				return null;
			}

			BaseOperation operation = null;

			if (dtoCalculation.Addition != null)
			{
				operation = ConvertDtoAddition(dtoCalculation.Addition.Terms);
			}
			else if (dtoCalculation.Division != null)
			{
				operation = ConvertDtoDivision(dtoCalculation.Division.Dividend, dtoCalculation.Division.Divisor);
			}
			else if (dtoCalculation.Exponential != null)
			{
				operation = ConvertDtoExponential(dtoCalculation.Exponential.PowerTerm);
			}
			else if (dtoCalculation.Logarithm != null)
			{
				operation = ConvertDtoLogarithm(dtoCalculation.Logarithm.LogTerm);
			}
			else if (dtoCalculation.Multiplication != null)
			{
				operation = ConvertDtoMultiplication(dtoCalculation.Multiplication.Terms);
			}
			else if (dtoCalculation.SquareRoot != null)
			{
				operation = ConvertDtoSquareRoot(dtoCalculation.SquareRoot.SqrtTerm);
			}
			else if (dtoCalculation.Subtraction != null)
			{
				operation = ConvertDtoSubtraction(dtoCalculation.Subtraction.Minuend, dtoCalculation.Subtraction.Subtrahend);
			}

			return operation;
		}

		static Addition ConvertDtoAddition(List<DtoTerm> dtoAdditionTerms)
		{
			if (dtoAdditionTerms == null)
			{
				return null;
			}

			List<Term> additionTerms = new List<Term>();

			foreach (DtoTerm dtoTerm in dtoAdditionTerms)
			{
				Term additionTerm = ConvertDtoTerm(dtoTerm);

				if (additionTerm != null)
				{
					additionTerms.Add(additionTerm);
				}
			}

			return new Addition(additionTerms);
		}

		static Division ConvertDtoDivision(DtoTerm dtoDividend, DtoTerm dtoDivisor)
		{
			return dtoDividend == null || dtoDivisor == null ? null : new Division(ConvertDtoTerm(dtoDividend), ConvertDtoTerm(dtoDivisor));
		}

		static Exponential ConvertDtoExponential(DtoTerm dtoPowerTerm)
		{
			return dtoPowerTerm == null ? null : new Exponential(ConvertDtoTerm(dtoPowerTerm));
		}

		static Logarithm ConvertDtoLogarithm(DtoTerm dtoLogTerm)
		{
			return dtoLogTerm == null ? null : new Logarithm(ConvertDtoTerm(dtoLogTerm));
		}

		static Multiplication ConvertDtoMultiplication(List<DtoTerm> dtoMultiplicationTerms)
		{
			if (dtoMultiplicationTerms == null)
			{
				return null;
			}

			List<Term> multiplicationTerms = new List<Term>();

			foreach (DtoTerm dtoTerm in dtoMultiplicationTerms)
			{
				Term multiplicationTerm = ConvertDtoTerm(dtoTerm);

				if (multiplicationTerm != null)
				{
					multiplicationTerms.Add(multiplicationTerm);
				}
			}

			return new Multiplication(multiplicationTerms);
		}

		static SquareRoot ConvertDtoSquareRoot(DtoTerm dtoSqrtTerm)
		{
			return dtoSqrtTerm == null ? null : new SquareRoot(ConvertDtoTerm(dtoSqrtTerm));
		}

		static Subtraction ConvertDtoSubtraction(DtoTerm dtoMinuend, List<DtoTerm> dtoSubtrahend)
		{
			Term minuend = dtoMinuend == null ? null : ConvertDtoTerm(dtoMinuend);

			List<Term> subtrahend = new List<Term>();

			if (dtoSubtrahend != null)
			{
				foreach (DtoTerm dtoTerm in dtoSubtrahend)
				{
					Term term = ConvertDtoTerm(dtoTerm);

					if (term != null)
					{
						subtrahend.Add(term);
					}
				}
			}

			return new Subtraction(minuend, subtrahend);
		}

		static Term ConvertDtoTerm(DtoTerm dtoTerm)
		{
			if (dtoTerm == null)
			{
				return null;
			}

			Term term = null;

			if (dtoTerm.Addition != null)
			{
				term = new Term(ConvertDtoAddition(dtoTerm.Addition.Terms));
			}
			else if (dtoTerm.Division != null)
			{
				term = new Term(ConvertDtoDivision(dtoTerm.Division.Dividend, dtoTerm.Division.Divisor));
			}
			else if (dtoTerm.Exponential != null)
			{
				term = new Term(ConvertDtoExponential(dtoTerm.Exponential.PowerTerm));
			}
			else if (dtoTerm.Logarithm != null)
			{
				term = new Term(ConvertDtoLogarithm(dtoTerm.Logarithm.LogTerm));
			}
			else if (dtoTerm.Multiplication != null)
			{
				term = new Term(ConvertDtoMultiplication(dtoTerm.Multiplication.Terms));
			}
			else if (dtoTerm.SquareRoot != null)
			{
				term = new Term(ConvertDtoSquareRoot(dtoTerm.SquareRoot.SqrtTerm));
			}
			else if (dtoTerm.Subtraction != null)
			{
				term = new Term(ConvertDtoSubtraction(dtoTerm.Subtraction.Minuend, dtoTerm.Subtraction.Subtrahend));
			}
			else if (dtoTerm.KnownVariable)
			{
				term = new Term(dtoTerm.KnownVariable);
			}
			else
			{
				term = new Term(dtoTerm.Value);
			}

			return term;
		}

		static List<QuantityConversion> ConvertDtoQuantityConversions(List<DtoQuantityConversion> dtoQuantityConversions)
		{
			if (dtoQuantityConversions == null)
			{
				return null;
			}

			List<QuantityConversion> quantitieConversions = new List<QuantityConversion>();

			foreach (DtoQuantityConversion dtoQuantityConversion in dtoQuantityConversions)
			{
				QuantityConversion quantityConversion = ConvertDtoQuantity(dtoQuantityConversion);

				if (quantityConversion != null)
				{
					quantitieConversions.Add(quantityConversion);
				}
			}

			return quantitieConversions;
		}

		static QuantityConversion ConvertDtoQuantity(DtoQuantityConversion dtoQuantityConversion)
		{
			return dtoQuantityConversion == null ? null : new QuantityConversion(dtoQuantityConversion.SourceQuantity,
																				 dtoQuantityConversion.DestinationQuantity,
																				 ConvertDtoCalculation(dtoQuantityConversion.Conversion));
		}
	}
}
