using HRMS.Model;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using ApexCharts;

namespace HRMS.Pages
{
    public partial class Generateslip
    {
        #region Properties
        [Parameter]
        public int ID { get; set; }
        private List<PayslipDetails> payslipdetails = new List<PayslipDetails>();
        #endregion
        protected override async Task OnInitializedAsync()
        {
            await LoadDataFromBackend();
        }
        #region IndividualPayroll
        private async Task LoadDataFromBackend()
        {
            try
            {
                string apiUrl = $"/api/Payroll/IndividualPayroll{ID}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<PayslipDetails>>();
                    payslipdetails = json;
                    StateHasChanged();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }
        #endregion

        #region NumberToWords
        private string ConvertToWords(decimal number)
        {
            return NumberToWords((int)number);
        }
        private string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (number < 20)
                    words += new[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" }[number];
                else
                {
                    words += new[] { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" }[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + new[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" }[number % 10];
                }
            }

            return words.Trim() + " ";
        }
        #endregion
        private void DownloadPDF()
        {
            JSRuntime.InvokeVoidAsync("downloadPDF");
        }
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }



    }
}