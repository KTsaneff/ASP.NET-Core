using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RESTful_API_Development_dotNET_Eight.Authority;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RESTful_API_Development_dotNET_Eight.Controllers
{
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthorityController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] AppCredential credential)
        {
            if (AppRepository.Authenticate(credential.ClientId, credential.Secret))
            {
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    access_token = CreateToken(credential.ClientId, expiresAt),
                    expires_at = expiresAt
                });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "Access unauthorized.");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status401Unauthorized
                };
                return new UnauthorizedObjectResult(problemDetails);
            }
        }

        private string CreateToken(string clientId, DateTime expiresAt)
        {
            //Algorithm to create token
            //Payload (claims)
            //Signing key

            var application = AppRepository.GetApplicationByClientId(clientId);

            var claims = new List<Claim>
            {
                new Claim("AppName", application?.ApplicationName??string.Empty),
                new Claim("Read", (application?.Scopes??string.Empty).Contains("read")?"true":"false"),
                new Claim("Write", (application?.Scopes??string.Empty).Contains("write")?"true":"false"),
            };

            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature),
                claims: claims,
                expires: expiresAt,
                notBefore: DateTime.UtcNow
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
