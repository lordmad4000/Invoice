namespace Users.API.Configuration
{
    public class CacheConfig
    {
        public int AbsoluteExpirationInHours { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
}