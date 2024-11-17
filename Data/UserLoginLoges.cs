namespace TrustWaveCarca.Data
{
    public class UserLoginLoges
    {
       
            public int Id { get; set; } // Primary Key
            public string UniqueId { get; set; } // The unique identifier for the user
            public string IpAddress { get; set; } // IP Address from where the login attempt was made
            public DateTime LoginTime { get; set; } // Time of login attempt
        

    }
}
