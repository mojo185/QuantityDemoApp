namespace QuantityDemoApp.Models
{
    internal class QuantityItem
    {
        public string? Key { get; init; }
        public string? Name { get; init; }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
