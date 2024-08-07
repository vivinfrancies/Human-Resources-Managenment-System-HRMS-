using MudBlazor;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;
using HRMS.Model;

namespace HRMS.Pages
{
	public partial class Login
	{
		#region Properties and Variables
		public string _Pass;
		List<HRMS.Model.LoginDetails> LoginEvent = new List<HRMS.Model.LoginDetails>();
		HRMS.Model.LoginDetails _login = new HRMS.Model.LoginDetails();
		public bool _loginDetails;
		public bool _newuserpass = true;
		string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput = InputType.Password;

		string PasswordInputIcon1 = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput1 = InputType.Password;

		string PasswordInputIcon2 = Icons.Material.Filled.VisibilityOff;
		InputType PasswordInput2 = InputType.Password;
		MudTextField<string> pwField1;
		MudForm form;
		bool success;
		bool isShow;
		bool isShow1;
		bool isShow2;
		public string error;

		AttendanceModel AttendanceModelObj = new AttendanceModel();
		#endregion

		#region Login Details
		public async Task LoginDetails()
		{
			try
			{
				string apiUrl = $"api/LoginDetails?user={_login.UserName}&password={_login.Password}";
				using var client = HttpClientFactory.CreateClient("API");

				var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadFromJsonAsync<List<HRMS.Model.LoginDetails>>();
					LoginEvent = json;
					await Newuser();
					//error = "Please Enter Correct Details";
					StateHasChanged();
				}
				else
				{
					error = "Login Not Found";
					Console.WriteLine("Login Failed");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message, Severity.Error);
			}
		}
		#endregion

		#region Set Password
		public async Task SetPassword()
		{
			try
			{
				var selectedLog = LoginEvent.FirstOrDefault(x => x.UserName == _login.UserName);
				_login.Id = selectedLog.Id;
				string apiUrl = $"api/LoginDetails/{_login.Id}";
				using var client = HttpClientFactory.CreateClient("API");
				var response = await client.PutAsJsonAsync(apiUrl, _login).ConfigureAwait(false);
				if (response.IsSuccessStatusCode)
				{
					_newuserpass = true;
					_loginDetails = false;
					Snackbar.Add("Password Changed Successfully", Severity.Success, config =>
					{
						config.VisibleStateDuration = 200;
					});
				}
				else
				{
					Snackbar.Add("Password Changed Failed", Severity.Error, config =>
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

		#region LoginAttendance
		public async Task LoginAttendance()
		{
			try
			{
				var selectedLeaveType = LoginEvent.FirstOrDefault(x => x.UserName == _login.UserName);
				var user = selectedLeaveType;
				AttendanceModelObj.EmpId = user.Id;
				string apiUrlAtt = "api/Attendances/PunchIn";
				using var clientAtt = HttpClientFactory.CreateClient("API");
				var responseAtt = await clientAtt.PostAsJsonAsync(apiUrlAtt, AttendanceModelObj).ConfigureAwait(false);
				if (responseAtt.IsSuccessStatusCode)
				{
					Console.WriteLine("PunchIn Successfully");
				}
				else
				{
					Console.WriteLine("PunchIn Failed");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception: {ex.Message}");
			}
		}
		#endregion

		#region Login Condition

		public async Task Newuser()
		{
			var selectedLeaveType = LoginEvent.FirstOrDefault(x => x.UserName == _login.UserName);
			var user = selectedLeaveType;

			if (user == null)
			{
				error = "Please Enter Correct Details";
			}
			else if (_login.UserName == _login.Password && _login.UserName.ToUpper() == user.UserName && _login.Password == user.Password)
			{
				_newuserpass = false;
				_loginDetails = true;
				error = "";
			}
			else if (_login.UserName.ToUpper() == user.UserName && _login.Password == user.Password && _login.UserName != _login.Password)
			{
				selectedLeaveType.Password = null;
				await LocalStore.SetItemAsync("user", selectedLeaveType);
				await LoginAttendance();
				NavigationManager.NavigateTo("/dash");

				Snackbar.Add($"{user.FirstName} Login Successfull", Severity.Success, config =>
				{
					config.VisibleStateDuration = 200;
				});
			}

			else if (_login.UserName != user.UserName || _login.Password != user.Password)
			{
				error = "Enter Correct Details";
			}
		}
		#endregion

		#region Password Visibility
		void ButtonTestclick()
		{
			if (isShow)
			{
				isShow = false;
				PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
				PasswordInput = InputType.Password;
			}
			else
			{
				isShow = true;
				PasswordInputIcon = Icons.Material.Filled.Visibility;
				PasswordInput = InputType.Text;
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

		void ButtonTestclick2()
		{
			if (isShow2)
			{
				isShow2 = false;
				PasswordInputIcon2 = Icons.Material.Filled.VisibilityOff;
				PasswordInput2 = InputType.Password;
			}
			else
			{
				isShow2 = true;
				PasswordInputIcon2 = Icons.Material.Filled.Visibility;
				PasswordInput2 = InputType.Text;
			}
		}

		#endregion

		#region New Password Condition

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
			if (!Regex.IsMatch(ForgetPassword, @"[!@#$%^&*()]"))
				yield return "Password must contain at least one Special Character";
		}

		private string PasswordMatch(string arg)
		{
			if (pwField1.Value != arg)
				return "Passwords don't match";
			return null;
		}
		#endregion

	}
}