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
                    CheckEasterEgg();
                };

                plusButton.Clicked += (s, e) =>
                {
                    currentValue++;
                    displayLabel.Text = currentValue.ToString();
                    CheckEasterEgg();
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
                        currentValue = customValue; //set to custom value
                        displayLabel.Text = currentValue.ToString(); //update display
                        customEntry.Text = ""; //clear entry field
                        CheckEasterEgg();
                    }
                };
            }
            else
            {
                displayLabel.Text = "00:00:00";
                displayLabel.FontSize = 24;
                HorizontalStackLayout timerButtons = new HorizontalStackLayout { Spacing = 10 };
                Button startPauseButton = new Button
                {
                    Text = "Start",
                    BackgroundColor = randomColor,
                    TextColor = Colors.White,
                    WidthRequest = 70,
                    HeightRequest = 40
                };

                Button resetButton = new Button
                {
                    Text = "Reset",
                    BackgroundColor = randomColor,
                    TextColor = Colors.White,
                    WidthRequest = 70,
                    HeightRequest = 40
                };

                timerButtons.Children.Add(startPauseButton);
                timerButtons.Children.Add(resetButton);
                topRow.Children.Add(timerButtons);

                System.Timers.Timer timer = new System.Timers.Timer(1000);
                TimeSpan elapsedTime = TimeSpan.Zero;
                bool isRunning = false;

                startPauseButton.Clicked += (s, e) =>
                {
                    if (!isRunning)
                    {
                        //start the timer
                        timer.Start();
                        startPauseButton.Text = "Pause";
                        isRunning = true;
                    }
                    else
                    {
                        //pause the timer
                        timer.Stop();
                        startPauseButton.Text = "Resume";
                        isRunning = false;
                    }
                };

                resetButton.Clicked += (s, e) =>
                {
                    timer.Stop(); //stop timer
                    elapsedTime = TimeSpan.Zero; //reset time
                    displayLabel.Text = "00:00:00"; //reset display
                    startPauseButton.Text = "Start"; //reset button text
                    isRunning = false; //set not running
                };

                timer.Elapsed += (s, e) =>
                {
                    elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1)); //add 1 second

                    //update display on main thread
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        displayLabel.Text = elapsedTime.ToString(@"hh\:mm\:ss");
                    });
                };

            }
            deleteButton.Clicked += (s, e) =>
            {
                countersLayout.Children.Remove(counterContent);
                CheckEasterEgg();
            };
            countersLayout.Children.Add(counterContent);
            topRow.Children.Add(deleteButton);
            CheckEasterEgg();
        }

        private void CheckEasterEgg()
        {
            var counters = countersLayout.Children.OfType<VerticalStackLayout>().ToList();
            if (counters.Count == 3)
            {
                bool hasNine = false, hasEleven = false, hasTwoThousandOne = false;
                foreach (var counter in counters)
                {
                    if (counter.Children.Count > 0 && counter.Children[0] is HorizontalStackLayout topRow)
                    {
                        // topRow children: [0]nameEntry, [1]displayLabel, [2+]buttons, [last]deleteButton
                        if (topRow.Children.Count > 1 && topRow.Children[1] is Label displayLabel)
                        {
                            if (int.TryParse(displayLabel.Text, out int value))
                            {
                                if (value == 9) hasNine = true;
                                if (value == 11) hasEleven = true;
                                if (value == 2001) hasTwoThousandOne = true;
                            }
                        }
                    }
                }
                if (hasNine && hasEleven && hasTwoThousandOne)
                {
                    Osama.IsVisible = true;
                    America.IsVisible = true;
                }
                else
                {
                    Osama.IsVisible = false;
                    America.IsVisible = false;
                }
            }
            else
            {
                Osama.IsVisible = false;
                America.IsVisible = false;
            }
        }

    }
}