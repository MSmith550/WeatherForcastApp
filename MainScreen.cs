using Newtonsoft.Json;
using System.Diagnostics;
using System;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForcastProgram
{
    public partial class MainScreen : Form
    {
        private string apiKey = "8113c99a58ae4c4fac510922241503";

        public MainScreen()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await button1_ClickAsync(sender, e);
        }

        private async Task button1_ClickAsync(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            string baseUrl = "https://api.weatherapi.com/v1";
            string endpoint = "/current.json";
            string location = textBoxCity.Text;
            string apiUrl = $"{baseUrl}{endpoint}?key={apiKey}&q={location}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                // Parse jsonString using Newtonsoft.Json or System.Text.Json
                // Deserialize the JSON response into C# objects representing weather data
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonString);

                labelLocation.Visible = true;
                labelCountry.Visible = true;
                labelTemp.Visible = true;
                labelCondition.Visible = true;
                pictureBoxWeatherIcon.Visible = true;

                labelLocation.Text = "Location: " + weatherData.location.name;
                labelCountry.Text = "Country: " + weatherData.location.country;
                labelTemp.Text = "Temperature: " + weatherData.current.temp_f.ToString() + "°F";
                labelCondition.Text = "Condition: " + weatherData.current.condition.text;

                // Display weather icon
                string iconUrl = "https:" + weatherData.current.condition.icon; // Assuming the icon URL needs to be prefixed with "https:"
                pictureBoxWeatherIcon.Load(iconUrl); // Load the image from the URL into the PictureBox control
            }
            else
            {
                // Handle error response
                // Example: Log the error, display an error message to the user, etc.
                MessageBox.Show("Failed to retrieve weather data. Status code: " + response.StatusCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await button2_ClickAsync(sender, e);
        }

        private async Task button2_ClickAsync(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            string baseUrl = "https://api.weatherapi.com/v1";
            string endpoint = "/forecast.json";
            string location = "Syracuse"; // Example location
            int days = 3;
            string apiUrl = $"{baseUrl}{endpoint}?key={apiKey}&q={location}&days={days}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(jsonString);
                // Parse jsonString using Newtonsoft.Json or System.Text.Json
                // Deserialize the JSON response into C# objects representing weather data
                ForcastWeatherData forecastWeatherData = JsonConvert.DeserializeObject<ForcastWeatherData>(jsonString);

                labelLocation1.Visible = true;
                labelCountry1.Visible = true;
                labelTemp1.Visible = true;
                labelCondition1.Visible = true;
                labelDate1.Visible = true;
                pictureBoxDayIcon1.Visible = true;

                labelTemp2.Visible = true;
                labelCondition2.Visible = true;
                labelDate2.Visible = true;
                pictureBoxDayIcon2.Visible = true;

                labelTemp3.Visible = true;
                labelCondition3.Visible = true;
                labelDate3.Visible = true;
                pictureBoxDayIcon3.Visible = true;

                labelLocation1.Text = "Location: " + forecastWeatherData.location.name;
                labelCountry1.Text = "Country: " + forecastWeatherData.location.country;

                int dayIndex = 0;

                if (forecastWeatherData != null && forecastWeatherData.forecast != null && forecastWeatherData.forecast.forecastday != null)
                {
                    foreach (var forecastDay in forecastWeatherData.forecast.forecastday)
                    {
                        Label tempLabel = Controls.Find($"labelTemp{dayIndex + 1}", true).FirstOrDefault() as Label;
                        Label conditionLabel = Controls.Find($"labelCondition{dayIndex + 1}", true).FirstOrDefault() as Label;
                        Label tempDate = Controls.Find($"labelDate{dayIndex + 1}", true).FirstOrDefault() as Label;
                        PictureBox iconPictureBox = Controls.Find($"pictureBoxDayIcon{dayIndex + 1}", true).FirstOrDefault() as PictureBox;

                        if (tempLabel != null && conditionLabel != null && iconPictureBox != null)
                        {
                            // Update the labels and picture box with the forecast data
                            tempLabel.Text = $"Temperature: {forecastDay.day.maxtemp_f}°F / {forecastDay.day.mintemp_f}°F";
                            conditionLabel.Text = $"Condition: {forecastDay.day.condition.text}";
                            tempDate.Text = $"Date: {forecastDay.date}";

                            // Load the weather icon from the URL into the picture box
                            string iconUrl = forecastDay.day.condition?.icon; // Assuming the icon URL is provided in the forecast data

                            if (iconUrl != null)
                            {
                                // Assuming the icon URL may be relative, prefix it with the base URL
                                iconUrl = "https:" + iconUrl;
                                iconPictureBox.Load(iconUrl);
                            }
                        }

                        dayIndex++;
                    }
                }
                else
                {
                    // Handle the case where one of the objects is null

                }
            }
            else
            {
                // Handle error response
                // Example: Log the error, display an error message to the user, etc.
                MessageBox.Show("Failed to retrieve weather data. Status code: " + response.StatusCode.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}