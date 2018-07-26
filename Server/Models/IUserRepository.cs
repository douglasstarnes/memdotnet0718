using Server.Models.ViewModels;

namespace Server.Models
{
    public interface IUserRepository
    {
         bool Register(RegisterViewModel registerViewModel);
         bool Login(LoginViewModel loginViewModel);
        string GenerateToken(LoginViewModel loginViewModel);
    }
}