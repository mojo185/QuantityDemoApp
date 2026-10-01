using QuantityDemoApp.Models;
using QuantitySystem;
using QuantitySystem.DataModels;
using QuantitySystem.DataModels.ReturnModel;
using System.Windows;
using System.Windows.Controls;
using IO = System.IO;

namespace QuantityDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _quantitySystemFile;

        private bool _updatingUnits;
        private bool _updatingCarUnits;
        private string? _previousDistanceUnit;
        private string? _previousConsumptionUnit;

        public MainWindow()
        {
            InitializeComponent();

            _quantitySystemFile = IO.Path.Combine(
                AppContext.BaseDirectory, "JsonExamples",
                "SimpleDemo.json");

            LoadQuantitySystem();
            LoadQuantities();
            LoadCarDemo();
        }

        private void LoadQuantitySystem()
        {
            try
            {
                if (!IO.File.Exists(_quantitySystemFile))
                {
                    MessageBox.Show(
                        $"Quantity system file was not found:\n\n{_quantitySystemFile}",
                        "Quantity System",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                QuantitySystemCore.LoadQuantitySystem(_quantitySystemFile);

                Title = "Quantity System Demo - Loaded";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The quantity system could not be loaded.\n\n{ex.Message}",
                    "Quantity System",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void LoadQuantities()
        {
            QuantityComboBox.Items.Clear();

            Dictionary<string, string> quantities =
                QuantitySystemCore.GetAllQuantities();

            foreach (KeyValuePair<string, string> quantity in quantities)
            {
                QuantityComboBox.Items.Add(
                    new QuantityItem
                    {
                        Key = quantity.Key,
                        Name = quantity.Value
                    });
            }

            if (QuantityComboBox.Items.Count > 0)
            {
                QuantityComboBox.SelectedIndex = 0;
            }
        }

        private void QuantityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuantityComboBox.SelectedItem is not QuantityItem quantity || string.IsNullOrWhiteSpace(quantity.Key))
                return;

            LoadUnits(quantity.Key);
        }

        private void LoadUnits(string quantityKey)
        {
            _updatingUnits = true;

            try
            {
                List<string> units =
                    QuantitySystemCore.GetQuantityUnits(quantityKey);

                FromUnitComboBox.Items.Clear();
                ToUnitComboBox.Items.Clear();

                foreach (string unit in units)
                {
                    FromUnitComboBox.Items.Add(unit);
                }

                if (FromUnitComboBox.Items.Count > 0)
                {
                    FromUnitComboBox.SelectedIndex = 0;
                }

                UpdateToUnits(units);
            }
            finally
            {
                _updatingUnits = false;
            }
        }

        private void FromUnitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updatingUnits)
                return;

            if (QuantityComboBox.SelectedItem is not QuantityItem quantity)
                return;

            List<string> units =
                QuantitySystemCore.GetQuantityUnits(quantity.Key);

            UpdateToUnits(units);
        }

        private void UpdateToUnits(List<string> units)
        {
            string? selectedFromUnit =
                FromUnitComboBox.SelectedItem as string;

            string? previousToUnit =
                ToUnitComboBox.SelectedItem as string;

            ToUnitComboBox.Items.Clear();

            foreach (string unit in units)
            {
                // Don't allow the same unit in both boxes.
                if (unit == selectedFromUnit)
                    continue;

                ToUnitComboBox.Items.Add(unit);
            }

            // Keep the previous "To" unit if it is still available.
            if (previousToUnit != null &&
                ToUnitComboBox.Items.Contains(previousToUnit))
            {
                ToUnitComboBox.SelectedItem = previousToUnit;
            }
            else if (ToUnitComboBox.Items.Count > 0)
            {
                ToUnitComboBox.SelectedIndex = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Check that a quantity has been selected.
            if (QuantityComboBox.SelectedItem is not QuantityItem quantityItem)
            {
                ErrorLabel.Content = "Please select a quantity.";
                return;
            }

            // Check that both units have been selected.
            if (FromUnitComboBox.SelectedItem is not string fromUnit)
            {
                ErrorLabel.Content = "Please select a source unit.";
                return;
            }

            if (ToUnitComboBox.SelectedItem is not string toUnit)
            {
                ErrorLabel.Content = "Please select a destination unit.";
                return;
            }

            // Parse the input value.
            if (!double.TryParse(
                    FromValueTextBox.Text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double value))
            {
                ErrorLabel.Content = "Please enter a valid number.";
                return;
            }

            try
            {
                // Get the actual Quantity object from the quantity system.
                Quantity quantity =
                    QuantitySystemCore.GetQuantity(quantityItem.Key);

                if (quantity == null)
                {
                    ErrorLabel.Content = "The selected quantity could not be found.";
                    return;
                }

                // Let the QuantitySystem perform the actual conversion.
                QuantityReturnModel result =
                    QuantitySystemCore.ConvertToUnit(
                        quantity,
                        fromUnit,
                        toUnit,
                        value);

                // Display the result.
                ToValueTextBox.Text = result.Value.ToString();

                ErrorLabel.Content = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorLabel.Content =
                    $"Conversion failed: {ex.Message}";
            }

        }

        private void FromValueTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ToValueTextBox != null)
                ToValueTextBox.Text = string.Empty;
        }

        // ------------------------------------------------
        // Real world
        // ------------------------------------------------
        private void LoadCarDemo()
        {
            Quantity distanceQuantity =
                QuantitySystemCore.GetQuantity("Length");

            Quantity consumptionQuantity =
                QuantitySystemCore.GetQuantity("FuelConsumption");

            if (distanceQuantity == null ||
                consumptionQuantity == null)
            {
                return;
            }
            _updatingCarUnits = true;

            try
            {
                LoadUnits(
                CarDistanceUnitComboBox,
                distanceQuantity.Key);

                LoadUnits(
                    CarConsumptionUnitComboBox,
                    consumptionQuantity.Key);

                CarDistanceTextBox.Text = "450";
                CarConsumptionTextBox.Text = "6,5";
            }
            finally
            {
                _updatingCarUnits = false;
            }
        }

        private void LoadUnits(ComboBox comboBox, string quantityKey)
        {
            comboBox.Items.Clear();

            List<string> units =
                QuantitySystemCore.GetQuantityUnits(quantityKey);

            foreach (string unit in units)
            {
                comboBox.Items.Add(unit);
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void CalculateFuelButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(
                    CarDistanceTextBox.Text,
                    out double distance))
            {
                CarFuelResultTextBlock.Text = "Invalid distance.";
                return;
            }

            if (!double.TryParse(
                    CarConsumptionTextBox.Text,
                    out double consumption))
            {
                CarFuelResultTextBlock.Text = "Invalid fuel consumption.";
                return;
            }

            string? distanceUnit =
                CarDistanceUnitComboBox.SelectedItem as string;

            string? consumptionUnit =
                CarConsumptionUnitComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(distanceUnit) ||
                string.IsNullOrEmpty(consumptionUnit))
            {
                CarFuelResultTextBlock.Text =
                    "Please select the required units.";

                return;
            }

            Quantity distanceQuantity =
                QuantitySystemCore.GetQuantity("Length");

            Quantity consumptionQuantity =
                QuantitySystemCore.GetQuantity("FuelConsumption");

            if (distanceQuantity == null ||
                consumptionQuantity == null)
            {
                CarFuelResultTextBlock.Text =
                    "Required quantities are not available.";

                return;
            }

            try
            {
                // Convert distance to the unit expected by the
                // fuel-consumption calculation.
                QuantityReturnModel distanceInKm =
                    QuantitySystemCore.ConvertToUnit(
                        distanceQuantity,
                        distanceUnit,
                        "km",
                        distance);

                // Convert fuel consumption to L/100km.
                QuantityReturnModel consumptionInLPer100Km =
                    QuantitySystemCore.ConvertToUnit(
                        consumptionQuantity,
                        consumptionUnit,
                        "L/100km",
                        consumption);

                // Extract the values from your QuantityReturnModel.
                double distanceKm = distanceInKm.Value;
                double consumptionLPer100Km = consumptionInLPer100Km.Value;

                double requiredFuel =
                    distanceKm * consumptionLPer100Km / 100.0;

                CarFuelResultTextBlock.Text =
                    $"{requiredFuel:F2} L";
            }
            catch (Exception ex)
            {
                CarFuelResultTextBlock.Text =
                    $"Calculation failed: {ex.Message}";
            }
        }

        private void CarDistanceUnitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updatingCarUnits)
            {
                if (CarDistanceUnitComboBox.SelectedItem is string initialUnit)
                    _previousDistanceUnit = initialUnit;
                return;
            }

            string? newUnit =
                CarDistanceUnitComboBox.SelectedItem as string;


            if (string.IsNullOrEmpty(newUnit) ||
                string.IsNullOrEmpty(_previousDistanceUnit))
            {
                _previousDistanceUnit = newUnit;
                return;
            }

            if (newUnit == _previousDistanceUnit)
                return;

            ConvertCarValue(
                CarDistanceTextBox,
                "Length",
                _previousDistanceUnit,
                newUnit);

            _previousDistanceUnit = newUnit;
        }

        private void CarConsumptionUnitComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_updatingCarUnits)
            {
                if (CarConsumptionUnitComboBox.SelectedItem is string initialUnit)
                    _previousConsumptionUnit = initialUnit;
                return;
            }

            string? newUnit =
                CarConsumptionUnitComboBox.SelectedItem as string;


            if (string.IsNullOrEmpty(newUnit) ||
                string.IsNullOrEmpty(_previousConsumptionUnit))
            {
                _previousConsumptionUnit = newUnit;
                return;
            }

            if (newUnit == _previousConsumptionUnit)
                return;

            ConvertCarValue(
                CarConsumptionTextBox,
                "FuelConsumption",
                _previousConsumptionUnit,
                newUnit);

            _previousConsumptionUnit = newUnit;
        }

        private void ConvertCarValue(TextBox textBox, string quantityKey, string fromUnit, string toUnit)
        {
            if (!double.TryParse(
                    textBox.Text,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out double value))
            {
                return;
            }

            Quantity quantity =
                QuantitySystemCore.GetQuantity(quantityKey);

            if (quantity == null)
                return;

            try
            {
                QuantityReturnModel result =
                    QuantitySystemCore.ConvertToUnit(
                        quantity,
                        fromUnit,
                        toUnit,
                        value);

                // Use the property you already identified in
                // QuantityReturnModel.
                textBox.Text = result.Value.ToString(
                    "0.##",
                    System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                // Keep the existing value if conversion fails.
            }
        }
    }
}