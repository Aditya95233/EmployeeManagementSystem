using EmployeeManagementSystem.AppDbContext;
using EmployeeManagementSystem.DTOS.Request;
using EmployeeManagementSystem.Entitys;
using EmployeeManagementSystem.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.Services
{
    public class UserDetailsService:IUserCredential
    {
        private readonly AppDbContext1 _context;
        private readonly IConfiguration _configuration;
        public UserDetailsService(AppDbContext1 appDbContext1,IConfiguration configuration)
        {
            _context = appDbContext1;
            _configuration = configuration;
        }
        public async Task CreateUser(CreateUser req)
        {
            var dep = await _context.Employee.AnyAsync(x => x.Id == req.EmployeeId);
            if (!dep) throw new UnauthorizedAccessException("That employee is not exist");
            var Hasher = new PasswordHasher<User>();
            //string pass = Hasher.HashPassword(req.Email, req.PasswordHash);
            var user = new User
            {
                Email = req.Email,
                EmployeeId = req.EmployeeId,
                Role = "Employee",
                IsActive =true
            };
            user.PasswordHash = Hasher.HashPassword(user, req.Password);
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task<string> Login(string email,string password)
        {
            var user = await _context.User.Where(u=>u.IsActive).FirstOrDefaultAsync(x => x.Email == email);
            var hashKey = new User
            {
                Email = user.Email,
                EmployeeId = user.EmployeeId,
                Role = user.Role,
                IsActive = user.IsActive
            };
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid user id or password");
            }
            return GenerateToken(user);
        }
        public string GenerateToken(User usr)
        {
            var secrateKey = _configuration["JWTCredential:SecreateKey"];
            var aud = _configuration["JWTCredential:aud"];
            var issu = _configuration["JWTCredential:issuer"];
            var SymentricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrateKey));
            var credential = new SigningCredentials(SymentricSecurityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,usr.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,usr.Email.ToString()),
                new Claim(ClaimTypes.Role,usr.Role)
            };
            var JwtToken = new JwtSecurityToken(
                issuer: issu,
                audience: aud,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(JwtToken);
        }
    }
}
