namespace kalkulator
{
    public partial class MainPage : ContentPage
    {
        private double _currentValue = 0;
        private double _lastValue = 0;
        private string _operator = "";
        private bool _isNewEntry = true;

        public MainPage()
        {
            InitializeComponent();  // Inicjalizacja komponentów XAML
        }

        void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            // Jeśli zaczynamy nowe wprowadzenie, wyczyść pole
            if (_isNewEntry || ResultText.Text == "0")
            {
                ResultText.Text = button.Text;
                _isNewEntry = false;
            }
            else
            {
                ResultText.Text += button.Text;
            }
        }

        void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            if (!double.TryParse(ResultText.Text, out _lastValue))
            {
                DisplayAlert("Błąd", "Nieprawidłowa liczba", "OK");
                return;
            }

            _operator = button.Text;
            _isNewEntry = true;
        }

        void OnEqualClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(ResultText.Text, out _currentValue))
            {
                DisplayAlert("Błąd", "Nieprawidłowa liczba", "OK");
                return;
            }

            double result = 0;

            try
            {
                switch (_operator)
                {
                    case "+":
                        result = _lastValue + _currentValue;
                        break;
                    case "-":
                        result = _lastValue - _currentValue;
                        break;
                    case "*":
                        result = _lastValue * _currentValue;
                        break;
                    case "/":
                        if (_currentValue != 0)
                            result = _lastValue / _currentValue;
                        else
                            DisplayAlert("Błąd", "Nie można dzielić przez zero", "OK");
                        break;
                    default:
                        DisplayAlert("Błąd", "Nie wybrano operatora", "OK");
                        return;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", ex.Message, "OK");
                return;
            }

            ResultText.Text = result.ToString();
            _isNewEntry = true;
        }

        void OnClearClicked(object sender, EventArgs e)
        {
            _currentValue = 0;
            _lastValue = 0;
            _operator = "";
            ResultText.Text = "0";
            _isNewEntry = true;
        }
    }
}