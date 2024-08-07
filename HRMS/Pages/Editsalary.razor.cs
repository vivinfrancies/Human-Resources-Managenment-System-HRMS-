using HRMS.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Formats.Asn1;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using static MudBlazor.CategoryTypes;

namespace HRMS.Pages
{
    public partial class Editsalary
    {
        #region Properties and Variables
        [Parameter]
        public string Code { get; set; }
        DateTime? date = DateTime.Today;
        private bool resetValueOnEmptyText;
        private bool coerceText;
        private bool coerceValue;
        private string EmpId { get; set; }
        private decimal taxPercentage { get; set; }
        private decimal pfPercentage { get; set; }

        private bool hasReimbursement { get; set; }
        private decimal netSalary { get; set; }

        decimal others;

        List<MonthSalCalc> monthsal = new List<MonthSalCalc>();
        List<SalaryDetails> finalsal = new List<SalaryDetails>();
        AccountDetails accountDetails = new AccountDetails();
        SalaryDetails saldetails = new SalaryDetails();


        #endregion


        protected override async Task OnInitializedAsync()
        {
            await LoadDataFromBackend();
            if (monthsal != null && monthsal.Count > 0)
            {
                ProcessMonthSalCalc();
            }
        }

        #region EmployeeDetails
        private async Task LoadDataFromBackend()
        {
            try
            {
                string apiUrl = $"api/Payroll/employeedetails/{Code}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<List<MonthSalCalc>>();
                    monthsal = json;
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }
        }

        #endregion
        /*private async Task<int?> GetEmployeeIdByCode(string code)
        {
            try
            {
                string apiUrl = $"api/Sample/employeedetails/{code}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var employee = await response.Content.ReadFromJsonAsync<MonthSalCalc>();
                    return employee.ID;
                }
                else
                {
                    Console.WriteLine("Failed to fetch employee details.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data: {ex.Message}");
                return null;
            }
        }*/

        #region MonthlySalaryCalculation
        private void ProcessMonthSalCalc()
        {
            foreach (var item in monthsal)
            {
                string role = item.Role;
                int experience = item.Experience;
                DateTime dateOfJoin = item.DateOfJoining;
                decimal basicSalary = item.BasicSalary;
                int leavecal = item.Leave;
                decimal reimburement = hasReimbursement ? item.Reimbursement : 0;
                int monthsWorked = ((DateTime.Now.Year - dateOfJoin.Year) * 12) + DateTime.Now.Month - dateOfJoin.Month;
                int sixMonthPeriods = monthsWorked / 6;

                decimal incrementAmount = role switch
                {
                    "hr" => 5000,
                    "admin" => 6000,
                    "team lead" => 4000,
                    "hod" => 3000,
                    "employee" => 2000,
                    _ => 0m
                };
                decimal leaveAmountPerDay = 600;

                if (leavecal > 6)
                {
                    others = leavecal - 6;
                    others = others * leaveAmountPerDay;

                }
                else
                {
                    others = 0;
                }
                basicSalary += incrementAmount * sixMonthPeriods;

                SalaryDetails salaryDetails = new SalaryDetails
                {
                    ID = item.ID,
                    EmpId = item.ID,
                    EmployeeCode = item.EmployeeCode,
                    BasicSalary = basicSalary,
                    /*HouseRent = 0,
                    Conveyance = 0,
                    OtherAllowance = 0,
                    ESI = 0,
                    Tax = 0,
                    PF = 0,*/
                    Others = others,
                    Reimbursement =  item.Reimbursement ,
                    NetSalary = basicSalary + item.Reimbursement - (0),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                finalsal.Add(salaryDetails);
            }
        }
        #endregion

        #region CalculateNetsalary
        private async void CalculateNetSalary()
        {
            var item = finalsal.FirstOrDefault();
            if (item == null) return;

            decimal reimbursementValue = hasReimbursement ? item.Reimbursement : 0;
            decimal taxValue = item.BasicSalary * (taxPercentage / 100);
            decimal pfValue = item.BasicSalary * (pfPercentage / 100);

            item.NetSalary = item.BasicSalary + item.HouseRent + item.Conveyance + item.OtherAllowance - (item.ESI + taxValue + pfValue + item.Others) + reimbursementValue;

            await ToSaveData();
            await updateAccountDetailsData(Code, saldetails);
            StateHasChanged();
        }

        #endregion

        #region SaveData
        public async Task ToSaveData()
        {
            try
            {
                saldetails.Tax = finalsal.FirstOrDefault()?.BasicSalary * taxPercentage / 100 ?? 0;
                saldetails.PF = finalsal.FirstOrDefault()?.BasicSalary * pfPercentage / 100 ?? 0;
                saldetails.EmpId = finalsal.FirstOrDefault().EmpId;
                foreach (var x in finalsal)
                {
                    saldetails.Reimbursement = x.Reimbursement;
                    saldetails.Others = x.Others;
                    saldetails.BasicSalary = x.BasicSalary;
                    saldetails.EmployeeCode = x.EmployeeCode;
                    saldetails.NetSalary=x.NetSalary;
                    string apiUrl = "api/Payroll";
                    using var client = HttpClientFactory.CreateClient("API");
                    var response = await client.PostAsJsonAsync(apiUrl, saldetails).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        Snackbar.Add("Saved Successfully");
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Snackbar.Add($"Failed: {response.StatusCode} - {errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Exception: {ex.Message}");
            }
        }

        #endregion

        #region Updatedata
        public async Task updateAccountDetailsData(string code, SalaryDetails accountDetails)
        {
            try
            {
                var selectedLog = finalsal.FirstOrDefault(x => x.EmployeeCode == accountDetails.EmployeeCode);
                accountDetails.BasicSalary = selectedLog.BasicSalary;
                accountDetails.EmployeeCode = code;
                string apiUrl = $"/api/Payroll/{code}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl, accountDetails).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add("Updated Successfully");
                }
                else
                {
                    Snackbar.Add("Failed To Update");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message);
            }
        }
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }
        #endregion


    }
}