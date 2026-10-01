
namespace QuantitySystem.DataModels
{
	public abstract class BaseOperation
	{
		protected const string BEGIN_BRACKET = "(";
		protected const string END_BRACKET = ")";

		internal abstract double Calculate(double valueToConvert);

		internal abstract string GenerateCalculationRepresentation();
	}
}
