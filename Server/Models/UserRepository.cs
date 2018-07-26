using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Server.Models.ViewModels;
using Server.Security;

namespace Server.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext ctx;
        public UserRepository(DataContext ctx)
        {
            this.ctx = ctx;

        }

        // encryption methods
        private string GetSalt()
        {
            var bytes = new byte[128 / 8];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private string GetHash(string password, string salt)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool ValidatePassword(string password, string salt, string hash)
        {
            var hashedString = GetHash(password, salt);
            return hashedString == hash;
        }

        // interface methods

        public bool Login(LoginViewModel loginViewModel)
        {
            var user = ctx.Users.Where(x => x.Username == loginViewModel.Username).FirstOrDefault();
            if (user == null) return false;
            return ValidatePassword(loginViewModel.Password, user.Salt, user.Hash);
        }

        public bool Register(RegisterViewModel registerViewModel)
        {
            var userExists = ctx.Users.Where(x => x.Username == registerViewModel.Username).Count() > 0;
            if (userExists) return false;

            var salt = GetSalt();
            var hash = GetHash(registerViewModel.Password, salt);

            var user = new User {
                Username = registerViewModel.Username,
                Salt = salt,
                Hash = hash,
                FavoriteColor = registerViewModel.FavoriteColor
            };

            ctx.Users.Add(user);
            ctx.SaveChanges();

            return true;
        }

        public string GenerateToken(LoginViewModel loginViewModel)
        {
            var user = ctx.Users.Where(x => x.Username == loginViewModel.Username).First();

            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString());
            var usernameClaim = new Claim(ClaimTypes.Name, user.Username);
            var favoriteColorClaim = new Claim(ClaimTypes.UserData, user.FavoriteColor);

            var securityKey = Keys.SigningKey;
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] { userIdClaim, usernameClaim, favoriteColorClaim }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var theToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(theToken);

            
        }
    }
}