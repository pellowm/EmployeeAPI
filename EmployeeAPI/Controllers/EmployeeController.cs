using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet("Employee")]
        public IEnumerable<EmployeeInformation> Get()
        {

            var results = new List<EmployeeInformation>();

            using (SqlConnection connection = new SqlConnection(
                 _config.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(@"SELECT 
	                                                  Info.[employee_id]
	                                                  ,[first_name]
	                                                  ,[last_name]
	                                                  ,[hire_date]
                                                      ,[phone_number]
                                                      ,[email]
                                                  FROM [EmployeeContact] as Contact
                                                  JOIN [EmployeeInformation] as Info
                                                  ON Contact.employee_id = Info.employee_id;", connection);

                SqlDataReader reader = command.ExecuteReader();
                //List<EmployeeInformation> employeeList = new List<EmployeeInformation>();
                while (reader.Read())
                {
                    EmployeeInformation employeeInformation = new EmployeeInformation()
                    {
                        employee_id = (int)reader["employee_id"],
                        first_name = reader["first_name"].ToString(),
                        last_name = reader["last_name"].ToString(),
                        hire_date = (reader["hire_date"] == DBNull.Value ? (DateTime?)null : (DateTime?)reader["hire_date"]),
                        phone_number = reader["phone_number"].ToString(),
                        email = reader["email"].ToString()
                    };
                    results.Add(employeeInformation);
                    //results.Add((EmployeeInformation)reader[0]);
                }
            }

            return results;

        }
    }
}