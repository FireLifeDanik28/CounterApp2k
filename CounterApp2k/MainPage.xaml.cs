namespace CounterApp2k
{
    public partial class MainPage : ContentPage
    {

        bool isTimer = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            //count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void AddCounterClicked(object sender, EventArgs e)
        {
            //add counter
        }

        private void IsTimerChecked(object sender, CheckedChangedEventArgs e)
        {
            if (isTimer == false)
                isTimer = true;
            else
                isTimer = false;

        }
    }
}
