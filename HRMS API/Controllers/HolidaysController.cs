using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;

        public HolidaysController(ModelContext DbContext, IConfiguration configuration)
        {
            _context = DbContext;
            _configuration = configuration;
        }

        [HttpPost]

        public async Task<ActionResult<Holiday>> PostTodoItem(Holiday todoDTO)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var sql = @"INSERT INTO Holidays ([HolidayName],[Date]) VALUES(@HolidayName,@HolidayNewDate)";
                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@HolidayName", todoDTO.HolidayName);
                        insertCommand.Parameters.AddWithValue("@HolidayNewDate", todoDTO.HolidayNewDate);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }


        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Holiday>> PutToItem(Holiday todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE Holidays SET HolidayName = @HolidayName, [Date] = @HolidayNewDate WHERE ID = @Id";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@Id", todoitem.Id);
                        updatecmd.Parameters.AddWithValue("@HolidayName", todoitem.HolidayName);
                        updatecmd.Parameters.AddWithValue("@HolidayNewDate", todoitem.HolidayNewDate);

                        await updatecmd.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpPut("Delete/{Id:int}")]
        public async Task<ActionResult<Holiday>> PutTo(Holiday todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE Holidays SET IsActive = 0 WHERE ID = @Id";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@Id", todoitem.Id);
                        updatecmd.Parameters.AddWithValue("@HolidayName", todoitem.HolidayName);
                        updatecmd.Parameters.AddWithValue("@HolidayNewDate", todoitem.HolidayNewDate);

                        await updatecmd.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetTodoItems()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<Holiday>();
                    var sql = @"SELECT * FROM Holidays WHERE IsActive = 1";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new Holiday
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    HolidayName = reader["HolidayName"].ToString(),
                                    HolidayNewDate = Convert.ToDateTime(reader["Date"]),
                                    HolidayDay = reader["Day"].ToString(),
                                };

                                projects.Add(project);
                            }
                        }
                    }
                    return Ok(projects);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
        }
    }
}