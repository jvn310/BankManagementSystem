using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Dynamic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;

namespace BankManagementSystem
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Logout")
            {
                // Confirm if the user wants to log out
                DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Show the login form
                    Login login = new Login();
                    login.Show();

                    // close dashboard form
                    this.Close();
                }
                else
                {
                    //revert back to first tab
                    tabControl1.SelectedIndex = 0; 
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = Convert.ToDecimal(textBox1.Text);
                string fromCurrency = comboBox1.SelectedItem.ToString();
                string toCurrency = comboBox2.SelectedItem.ToString();

                CurrencyConverter converter = new CurrencyConverter();
                decimal convertedAmount = await converter.ConvertCurrency(amount, fromCurrency, toCurrency);

                // Display the result in a MessageBox
                MessageBox.Show($"Converted Amount: {convertedAmount} {toCurrency}",
                    "Conversion Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show($"Error: {ex.Message}",
                    "Conversion Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clear the input fields
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        public class CurrencyConverter
        {
            private static readonly HttpClient client = new HttpClient();

            public async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
            {
                string apiKey = "707b5d148b86d829d8cb4114";
                string url = $"https://v6.exchangerate-api.com/v6/{apiKey}/pair/{fromCurrency}/{toCurrency}";

                // Make the API request
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                // Check if jsonResponse is not null
                if (jsonResponse != null)
                {
                    // Extract the conversion rate
                    decimal exchangeRate = jsonResponse.conversion_rate;

                    // Return the converted amount
                    return amount * exchangeRate;
                }
                else
                {
                    throw new Exception("Failed to parse JSON response");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //terminate program
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //terminate program
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //terminate program
            Application.Exit();
        }
    }
}


