   using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HormoneTracker.DAL;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace HormoneTracker.Controllers
{
    [Route("api/Security/Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly HormoneTrackerDBContext _context;
        private readonly IConfiguration _configuration;

        private string role;
        private string userId;

        public TokenController(HormoneTrackerDBContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Verify(Dictionary<string, string> credentials)
        {
            try
            {
                if (Validate(credentials))
                {
                    string issuer = _configuration.GetValue<string>("Jwt:Issuer");
                    string audience = _configuration.GetValue<string>("Jwt:Audience");
                    byte[] key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
                    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Expires = DateTime.UtcNow.AddDays(1),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    var result = new Dictionary<string, string>()
                    {
                        { "Token", stringToken },
                        { "Role", role },
                        { "UserId", GetUserId(credentials["Email"], role).ToString() }
                    };


                    return Ok(result);
                }
                return Unauthorized("Acces denied");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool Validate(Dictionary<string, string> credentials)
        {
            string email = credentials["Email"];
            string pass = credentials["Password"];
            bool validated = false;

            validated |= _context.Admins.Where(a => a.Email.Equals(email) && a.Password.Equals(pass)).Count() == 1;
            if (validated)
            {
                role = "admin";
                return validated;
            }


            validated |= _context.Doctors.Where(a => a.Email.Equals(email) && a.Password.Equals(pass)).Count() == 1;
            if (validated)
            {
                role = "doctor";
                return validated;
            }

            validated |= _context.Patients.Where(a => a.Email.Equals(email) && a.Password.Equals(pass)).Count() == 1;
            if (validated)
                role = "patient";
            
            return validated;
        }

        private int GetUserId(string email, string role)
        {
            if (role == "admin")
            {
                return _context.Admins.First(a => a.Email == email).AdminId;
            }
            else if (role == "doctor")
            {
                return _context.Doctors.First(d => d.Email == email).DoctorId;
            }
            else
            {
                return _context.Patients.First(p => p.Email == email).PatientId;
            }
        }
    }
}
