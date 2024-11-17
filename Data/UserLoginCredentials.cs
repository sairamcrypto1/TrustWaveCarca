using System.ComponentModel.DataAnnotations;

namespace TrustWaveCarca.Data
{
    public class UserLoginCredentials
    {

        [Key]
        public int Id { get; set; }  // Common auto-increment ID
        public string UniqueLoginID { get; set; }

        public string Email { get; set; }

        public string MobileNo { get; set; }

        public DateOnly RegistrationDate { get; set; }

        public bool IsDelete { get; set; }=false;


    }
}
