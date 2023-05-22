using System;

namespace Invoice.Infra.Configuration
{
    public class JWTConfig
    {
        public const string JWT = "JWTConfig";
        public string SecretKey { get; set; } = String.Empty;

    }
}