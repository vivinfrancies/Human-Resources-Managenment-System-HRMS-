using HRMS.Model;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace HRMS.Pages
{
    public partial class ReimbursementRequest
    {
        public bool Visible;
        public bool valueD = false;
        public string SearchRequests;
        void Submit() => Visible = false;


        HRMS.Model.Reimbursement reimbursement = new HRMS.Model.Reimbursement();

        List<HRMS.Model.Reimbursement> Reimburstmentrequesttable = new List<HRMS.Model.Reimbursement>();

        //OnInitializedAsync
        protected override async Task OnInitializedAsync()
        {
            var userJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "user");
            User = JsonSerializer.Deserialize<LoginDetails>(userJson);

            await Othersclaim();
        }



        //To get Others Claim
        public async Task Othersclaim()
        {
            string apiUrl = $"api/ReimburstmentRequest/Others Claim Requests{User.UserName}";
            using var client = HttpClientFactory.CreateClient("API");

            var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.Reimbursement>>();
                Reimburstmentrequesttable = json;
                StateHasChanged();
            }
            else
            {
                Snackbar.Add("Failed");
            }
        }

        //To Search Data in DataGrid
        public Func<HRMS.Model.Reimbursement, bool> QuickFilterRequests => z =>
        {
            if (string.IsNullOrWhiteSpace(SearchRequests))
                return true;

            if ($"{z.EmpCode}{z.FirstName}{z.Designation}{z.Expense}".Contains(SearchRequests, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        //To Approve Request
        public async Task Approvebut(int Id)
        {
            var name = Reimburstmentrequesttable.FirstOrDefault(x => x.Id == Id);

            if (name != null)
            {

                string apiUrl5 = $"api/ReimburstmentRequest/Approve /{name.Id}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl5, reimbursement).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {

                    Snackbar.Add($"{name.FirstName}'s Claim Request Approved", Severity.Success);
                    name.Status = "Approved";
                    name.Accpt = true;
                    name.declin = true;
                    await Othersclaim();
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed");
                }

            }
            else
            {
                throw new Exception("No matching claim request found");
            }
        }

        //To Reject Request
        public async void Rejectbut(int id)
        {
            var reject = Reimburstmentrequesttable.FirstOrDefault(x => x.Id == id);

            if (reject == null)
            {
                throw new Exception("No matching claim request found");
            }
            else
            {

                string apiUrl5 = $"api/ReimburstmentRequest/Reject / {reject.Id}";
                using var client = HttpClientFactory.CreateClient("API");
                var response = await client.PutAsJsonAsync(apiUrl5, reimbursement).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {

                    Snackbar.Add($"{reject.FirstName}'s claim Request Rejected", Severity.Error);
                    reject.Status = "Rejected";
                    reject.Accpt = true;
                    reject.declin = true;
                    await Othersclaim();
                    StateHasChanged();
                }
                else
                {
                    Snackbar.Add("Failed");
                }
            }
        }

        //Dialogue Box 
        public void opendialogue(int id)
        {
            Visible = true;
            var name = Reimburstmentrequesttable.FirstOrDefault(x => x.Id == id);
            reimbursement.Bill = name.Bill;
            reimbursement.Roles = name.Roles;
        }


        //GoBack
        private async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }
    }
}