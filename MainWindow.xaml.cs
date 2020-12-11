using System;
using System.Windows;

namespace AutoFish
{
    public partial class MainWindow : Window
    {
        private readonly IAuto auto;
        private const int ONE_THROW_COST = 5; // стоимость одного заброса удочки
        private const int MAX_OP = 5000;
        private const int MIN_OP = 5;

        public MainWindow()
        {
            InitializeComponent();
            auto = new AutoArcheRage(this.Title);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var count = Convert.ToInt32(textBoxTotal.Text);
                if(count > MAX_OP || count < MIN_OP || count % 5 != 0)
                {
                    throw new Exception();
                }

                count /= ONE_THROW_COST;
                auto.Start(count, UpdateProgressBar);
                progressBar.Maximum = count;
            }
            catch (Exception)
            {
                auto.Stop();
                MessageBox.Show("Введите корректное количесто ОР.");
                return;
            }

            btnStart.IsEnabled = false;
            progressBar.Value = progressBar.Minimum;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = true;
            auto.Stop();
        }

        private void BtnSetAll_Click(object sender, RoutedEventArgs e)
        {
            textBoxTotal.Text = MAX_OP.ToString();
        }

        private void UpdateProgressBar()
        {
            Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                var countLeft = auto.GetCountLeft();
                textBoxTotal.Text = (countLeft * ONE_THROW_COST).ToString();
                btnStart.IsEnabled = countLeft <= 0;
            });
        }
    }
}
