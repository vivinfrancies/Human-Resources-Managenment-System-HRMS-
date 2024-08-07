using HRMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolenDesignationController : Controller
    {

        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;

        public RolenDesignationController(ModelContext DbContext, IConfiguration configuration)
        {
            _context = DbContext;
            _configuration = configuration;
        }
        [HttpGet("Role")]
        public async Task<ActionResult<RolenDesignation>> ResignationTableview()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var ResignationTableview = new List<RolenDesignation>();
                    var sql = @"SELECT * FROM Roles";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {


                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Populate the Employee object properties

                                var Resignationview = new RolenDesignation
                                {
                                    RoleId = Convert.ToInt32(reader["ID"]),
                                    RoleName = reader["Role"].ToString()

                                };

                                ResignationTableview.Add(Resignationview);

                            }
                            return Ok(ResignationTableview);
                        }
                        // Return the populated Employee object     
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }



        [HttpGet("Designation")]
        public async Task<ActionResult<RolenDesignation>> esignationTableview()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var ResignationTableview = new List<RolenDesignation>();
                    var sql = @"SELECT * FROM Designations";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {


                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                // Populate the Employee object properties

                                var Resignationview = new RolenDesignation
                                {
                                    DesignationId = Convert.ToInt32(reader["ID"]),
                                    DesignationName = reader["Designation"].ToString()

                                };

                                ResignationTableview.Add(Resignationview);

                            }
                            return Ok(ResignationTableview);
                        }
                        // Return the populated Employee object     
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


    }
}