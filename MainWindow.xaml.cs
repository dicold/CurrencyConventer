using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConventerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrencyInsert.Text))
                CurrencyOutcome.Text = "Input string was empty";
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7140/api/Conventer?number={CurrencyInsert.Text}");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    CurrencyOutcome.Text = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    CurrencyOutcome.Text = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
