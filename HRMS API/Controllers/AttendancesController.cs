using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;
using HRMS_API.Model;


namespace HRMS_API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AttendancesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GET AttendanceView
        [HttpGet("AttendanceView")]
        public async Task<ActionResult<IEnumerable<AttendanceModel>>> Get()
        {
            try
            {
                var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");
                using (var appDbConnection = new SqlConnection(appDbConnectionString))
                {
                    await appDbConnection.OpenAsync();
                    var attendanceList = new List<AttendanceModel>();
                    var sql = @"SELECT * FROM DailyAttendance ORDER BY [Id] Desc;";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var attendance = new AttendanceModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    EmployeeCode = reader["EmployeeCode"].ToString(),
                                    EmployeeName = reader["EmployeeName"].ToString(),
                                    date = Convert.ToDateTime(reader["Date"]),
                                    PunchIn = reader["PunchIn"].ToString(),
                                    PunchOut = reader["PunchOut"].ToString(),
                                    WorkingHours = reader["WorkingHours"].ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                };
                                attendanceList.Add(attendance);
                            }
                        }
                    }
                    return Ok(attendanceList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GET TotalPresent
        [HttpGet("TotalPresent")]
        public async Task<ActionResult<IEnumerable<AttendanceTotalPresent>>> GetPresent()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var attendanceList = new List<AttendanceTotalPresent>();
                    var sql = @"SELECT Count(*) AS TotalEntry FROM TotalEntry;";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var attendance = new AttendanceTotalPresent
                                {
                                    TotalPresent = Convert.ToInt32(reader["TotalEntry"]),
                                };
                                attendanceList.Add(attendance);
                            }
                        }
                    }
                    return Ok(attendanceList);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        #endregion

        #region GET BY ID Employee Tab
        [HttpGet("SearchByID/{EmpId:int}")]
        public async Task<ActionResult<AttendanceModel>> Get(int EmpId)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                var attendanceList = new List<AttendanceModel>();
				var sql = @"SELECT TOP 7 * FROM DailyAttendance WHERE EmpId=@EmpId ORDER BY [Id] Desc;";
				using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    try
                    {
                        selectCommand.Parameters.AddWithValue("@EmpId", EmpId);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var attendance = new AttendanceModel
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    EmployeeCode = reader["EmployeeCode"].ToString(),
                                    EmployeeName = reader["EmployeeName"].ToString(),
                                    date = Convert.ToDateTime(reader["Date"]),
                                    PunchIn = reader["PunchIn"].ToString(),
                                    PunchOut = reader["PunchOut"].ToString(),
                                    WorkingHours = reader["WorkingHours"].ToString(),
                                };
                                attendanceList.Add(attendance);
                            }
                            return Ok(attendanceList);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
        }
        #endregion

        #region POST PunchIn
        [HttpPost("PunchIn")]
        public async Task<ActionResult<AttendanceModel>> PostHRMSItem(AttendanceModel HRMSDTO)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var sql = @"INSERT INTO Attendance ([EmpId], [Date], [PunchIn]) VALUES(@EmpId,convert(Date, getdate()),convert(varchar, getdate(), 24))";
                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@EmpId", HRMSDTO.EmpId);
                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        #endregion

        #region PUT PunchOut
        [HttpPut("PunchOut/{EmpId:int}")]
        public async Task<ActionResult<AttendanceModel>> PutPunchOut(AttendanceModel HRMSitem)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                try
                {
                    await appDbConnection.OpenAsync();
                    var sql = @"EXEC PunchOutUpdate @EmpId = @EmpId";
                    using (var UpdateCommand = new SqlCommand(sql, appDbConnection))
                    {
                        UpdateCommand.Parameters.AddWithValue("@EmpId", HRMSitem.EmpId);
                        await UpdateCommand.ExecuteNonQueryAsync();
                        return Ok(true);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        #endregion

        #region PUT Update
        [HttpPut("Update/{Id:int}")]
        public async Task<ActionResult<AttendanceModel>> PutUpdate(AttendanceModel HRMSitem)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                try
                {
                    await appDbConnection.OpenAsync();
                    var sql = @"EXEC UpdateAll @Id = @Id, @date = @date, @PunchIn = @PunchIn, @PunchOut = @PunchOut;";
                    using (var UpdateCommand = new SqlCommand(sql, appDbConnection))
                    {
                        UpdateCommand.Parameters.AddWithValue("@Id", HRMSitem.Id);
                        UpdateCommand.Parameters.AddWithValue("@date", HRMSitem.date);
                        UpdateCommand.Parameters.AddWithValue("@PunchIn", HRMSitem.PunchIn);
                        UpdateCommand.Parameters.AddWithValue("@PunchOut", HRMSitem.PunchOut);
                        await UpdateCommand.ExecuteNonQueryAsync();
                        return Ok(true);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
        #endregion
    }
}