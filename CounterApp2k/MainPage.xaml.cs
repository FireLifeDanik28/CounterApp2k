namespace CounterApp2k
{
    public partial class MainPage : ContentPage
    {
        //czy ma spawnowac tajmer czy licznik
        private bool isTimer = false;
        //ile jest obecnie dodanych counterow
        private int counterCount = 0;
        //div z tajmerami do scrollview
        private VerticalStackLayout countersLayout;

        public MainPage()
        {
            InitializeComponent();
            //dodaje przestrzen miedzy counterami
            countersLayout = new VerticalStackLayout { Spacing = 15 };
            //wsadza to do scrollview
            CounterView.Content = countersLayout;
        }
        //myslalem ze uzyje ale jednak nie
        //private void OnCounterClicked(object? sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}

        private void AddCounterClicked(object sender, EventArgs e)
        {
            //add counter
            AddCounter();
        }

        private void IsTimerChecked(object sender, CheckedChangedEventArgs e)
        {
            //timer jest true gdy checkbox jest zaznaczony
            if (isTimer == false)
                isTimer = true;
            else
                isTimer = false;
        }

        private void AddCounter()
        {
            counterCount++;
            Random random = new Random();
            Color randomColor = Color.FromRgb(
                random.Next(50, 240),//r
                random.Next(50, 240),//g
                random.Next(50, 240)//b
                );
            VerticalStackLayout counterContent = new VerticalStackLayout { Spacing = 8 };
            HorizontalStackLayout topRow = new HorizontalStackLayout { Spacing = 10 };
            Entry nameEntry = new Entry
            {
                Placeholder = $"Counter {counterCount}",
                TextColor = randomColor,
                BackgroundColor = Colors.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold
            };
            Button deleteButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                WidthRequest = 40,
                HeightRequest = 40
            };
            topRow.Children.Add(nameEntry);
            counterContent.Children.Add(topRow);
            Label displayLabel = new Label
            {
                Text = "0",
                FontSize = 32,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = randomColor,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };
            topRow.Children.Add(displayLabel);
            if (!isTimer)
            {
                HorizontalStackLayout counterButtons = new HorizontalStackLayout { Spacing = 10 };
                Button minusButton = new Button
                {
                    Text = "-",
                    BackgroundColor = randomColor,
                    TextColor = Colors.White,
                    WidthRequest = 40,
                    HeightRequest = 40
                };
                Button plusButton = new Button
                {
                    Text = "+",
                    BackgroundColor = randomColor,
                    TextColor = Colors.White,
                    WidthRequest = 40,
                    HeightRequest = 40
                };
                counterButtons.Children.Add(minusButton);
                counterButtons.Children.Add(plusButton);
                topRow.Children.Add(counterButtons);

                int currentValue = 0;
                minusButton.Clicked += (s, e) =>
                {
                    currentValue--;
                    displayLabel.Text = currentValue.ToString();
                };

                plusButton.Clicked += (s, e) =>
                {
                    currentValue++;
                    displayLabel.Text = currentValue.ToString();
                };

                Entry customEntry = new Entry
                {
                    Placeholder = "Set value",
                    TextColor = randomColor,
                    BackgroundColor = Colors.White,
                    WidthRequest = 80,
                    Keyboard = Keyboard.Numeric
                };

                Button applyButton = new Button
                {
                    Text = "Apply",
                    BackgroundColor = randomColor,
                    TextColor = Colors.White,
                    WidthRequest = 60,
                    HeightRequest = 40
                };

                topRow.Children.Add(customEntry);
                topRow.Children.Add(applyButton);

                applyButton.Clicked += (s, e) =>
                {
                    if (int.TryParse(customEntry.Text, out int customValue))
                    {
                        currentValue = customValue; // Set to custom value
                        displayLabel.Text = currentValue.ToString(); // Update display
                        customEntry.Text = ""; // Clear entry field
                    }
                };
            }
            else
            {

            }
            deleteButton.Clicked += (s, e) =>
            {
                countersLayout.Children.Remove(counterContent);
                //
            };
            countersLayout.Children.Add(counterContent);
            topRow.Children.Add(deleteButton);
        }
    }
}