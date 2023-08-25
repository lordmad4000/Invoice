using System;

namespace SimplexInvoice.Infra.Configuration
{
    public class JWTConfig
    {
        public const string JWT = "JWTConfig";
        public string SecretKey { get; set; } = String.Empty;

    }
}