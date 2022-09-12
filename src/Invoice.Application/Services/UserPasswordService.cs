using Invoice.Application.Interfaces;
using Invoice.Domain.Interfaces;

namespace Invoice.Application.Services
{

    public class UserPasswordService : IPasswordService
    {
        private readonly IPasswordEncryption _passwordEncryptionService;

        public UserPasswordService(IPasswordEncryption passwordEncryptionService)
        {
            _passwordEncryptionService = passwordEncryptionService;
        }

        public string GeneratePassword(string userName, string password, int saltLength)
        {
            var salt = _passwordEncryptionService.GenerateSalt(saltLength);
            var hash = _passwordEncryptionService.GenerateHash($"{userName}{password}", salt);

            return $"{salt},{hash}";
        }

        public bool IsCorrectPassword(string userName, string saltHashPassword, string password)
        {
            var splitSaltHashPassword = saltHashPassword.Split(",");
            var hash = _passwordEncryptionService.GenerateHash($"{userName}{password}", splitSaltHashPassword[0]);

            return hash.Equals(splitSaltHashPassword[1]);
        }

    }
}