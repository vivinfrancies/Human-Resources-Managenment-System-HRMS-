using HRMS.Model;
using MudBlazor;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Pages
{
	public partial class ForgetPassword
	{

		#region Properties and Variables
		List<HRMS.Model.ForgetPassword> Forgetpaas = new List<HRMS.Model.ForgetPassword>();
		HRMS.Model.ForgetPassword PasswordEvent = new HRMS.Model.ForgetPassword();
		HRMS.Model.LoginDetails _login = new HRMS.Model.LoginDetails();
		public string error = "";
		public int a;
		public string Opt;
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter the valid email address")]
		public string ForgetEmail { get; set; }
		public string Password0 { get; set; }
		public string Password1 { get; set; }
		bool success;
		string[] errors = { };
		MudForm form;
		bool ShowEmail = false;
		bool ShowOtp = true;
		bool ShowPassword = true;
		MudTextField<string> pwField1;
		bool isShow0;
		InputType PasswordInput0 = InputType.Password;
		string PasswordInputIcon0 = Icons.Material.Filled.VisibilityOff;
		bool isShow1;
		InputType PasswordInput1 = InputType.Password;
		string PasswordInputIcon1 = Icons.Material.Filled.VisibilityOff;
		#endregion

		#region Check Email
		public async Task EmailCheck()
		{
			try
			{
				string apiUrl = $"api/LoginDetails/{PasswordEvent.Email}";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.ForgetPassword>>();
					Forgetpaas = json;
					StateHasChanged();
				}
				else
				{
					Snackbar.Add("Email Not Match", Severity.Error, config =>
					{
						config.VisibleStateDuration = 200;
					});
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message, Severity.Error);
			}
		}
		#endregion

		#region Forget Password
		public async Task Change()
		{
			var checkpass = Forgetpaas.FirstOrDefault(x => x.Email == PasswordEvent.Email);
			if (checkpass.Password == _login.NewPassword)
			{
				Snackbar.Add("Old Password and New Password Should Not Be Same", Severity.Error, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}
			else if (checkpass != null && _login.NewPassword == Password0 && checkpass.Password != _login.NewPassword)
			{
				try
				{
					_login.Id = checkpass.Id;
					string apiUrl = $"api/LoginDetails/{checkpass.Id}";
					using var client = HttpClientFactory.CreateClient("API");
					var response = await client.PutAsJsonAsync(apiUrl, _login).ConfigureAwait(false);
					if (response.IsSuccessStatusCode)
					{
						NavigationManager.NavigateTo("/");
						Snackbar.Add("Password Changed Succesfully", Severity.Success, config =>
						{
							config.VisibleStateDuration = 200;
						});
					}
					else
					{
						Snackbar.Add("Password Change Failed", Severity.Error, config =>
						{
							config.VisibleStateDuration = 200;
						});
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			else if (_login.NewPassword != Password0)
			{
				Snackbar.Add("Password Should Be Same", Severity.Error, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}
		}
		#endregion

		#region Two Password Check 
		private IEnumerable<string> PasswordStrength1(string ForgetPassword)
		{
			if (string.IsNullOrWhiteSpace(ForgetPassword))
			{
				yield return "Password is required!";
				yield break;
			}
			if (ForgetPassword.Length < 8)
				yield return "Password must be at least of length 8";
			if (!Regex.IsMatch(ForgetPassword, @"[A-Z]"))
				yield return "Password must contain at least one capital letter";
			if (!Regex.IsMatch(ForgetPassword, @"[a-z]"))
				yield return "Password must contain at least one lowercase letter";
			if (!Regex.IsMatch(ForgetPassword, @"[0-9]"))
				yield return "Password must contain at least one digit";
		}

		private string PasswordMatch(string arg)
		{
			if (pwField1.Value != arg)
				return "Passwords don't match";
			return null;
		}
		#endregion

		#region OTP and Verify Email
		public async Task OtpBtn()
		{
			await EmailCheck();
			var selectedLeaveType = Forgetpaas.FirstOrDefault(x => x.Email == PasswordEvent.Email);
			var email = selectedLeaveType;

			if (email == null)
			{
				error = "Please enter Valid Email";
			}
			else if (email.Email == PasswordEvent.Email)
			{
				ShowEmail = true;
				ShowOtp = false;
				Snackbar.Add("OTP send successfull.", Severity.Success, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}
			else
			{
				error = "Please enter Valid Email";
			}

		}
		#endregion

		#region Check OTP
		void VerifyBtn()
		{
			if (Opt == "123456")
			{
				Snackbar.Add("OTP Verified Successfully", Severity.Success, config =>
				{
					config.VisibleStateDuration = 200;
				});
				ShowEmail = true;
				ShowPassword = false;
				ShowOtp = true;
			}
			else if (Opt == null)
			{
				Snackbar.Add("OTP field is Empty", Severity.Error, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}
			else
			{
				Snackbar.Add("Invalid OTP", Severity.Error, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}
		}
		#endregion

		#region Password Visibility
		void ButtonTestclick0()
		{
			if (isShow0)

			{
				isShow0 = false;
				PasswordInputIcon0 = Icons.Material.Filled.VisibilityOff;
				PasswordInput0 = InputType.Password;
			}

			else
			{
				isShow0 = true;
				PasswordInputIcon0 = Icons.Material.Filled.Visibility;
				PasswordInput0 = InputType.Text;
			}
		}

		void ButtonTestclick1()
		{
			if (isShow1)

			{
				isShow1 = false;
				PasswordInputIcon1 = Icons.Material.Filled.VisibilityOff;
				PasswordInput1 = InputType.Password;
			}

			else
			{
				isShow1 = true;
				PasswordInputIcon1 = Icons.Material.Filled.Visibility;
				PasswordInput1 = InputType.Text;
			}
		}
		#endregion
	}
}