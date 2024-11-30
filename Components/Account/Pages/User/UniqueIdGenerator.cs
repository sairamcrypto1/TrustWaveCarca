using Microsoft.AspNetCore.Server.HttpSys;

namespace TrustWaveCarca.Components.Account.Pages.User
{
    public class UniqueIdGenerator
    {
        private static Random _random = new Random();

        public static string GenerateUniqueId(string mobileNumber, string email)
        {
            if (mobileNumber.Length < 2 || email.Length < 2)
            {
                throw new ArgumentException("Mobile number and email must have at least 2 characters.");
            }

            // Extract 2 digits from the mobile number (e.g., last two digits)
            string mobilePart = mobileNumber.Substring(mobileNumber.Length - 2);

            // Extract 2 characters from the email (e.g., first 2 characters)
            string emailPart = email.Substring(0, 2);

            // Extract 2 characters from "TrustWaveCarca" (e.g., first 2 characters)
            string trustWavePart = "TrustWaveCarca".Substring(0, 2);

            // Generate 2 random numeric digits
            string randomPart = _random.Next(10, 99).ToString();

            // Combine the parts into a single string
            string combinedId = mobilePart + emailPart + trustWavePart + randomPart;

            // Shuffle the characters to create a jumbled ID
            string uniqueId = new string(combinedId.OrderBy(c => _random.Next()).ToArray());

            return uniqueId;
        }
        private static Random random = new Random();  // Use a static Random instance

        public static string GenerateChatReqTransactionId(string SedId, string RecId)
        {
             int randomNumber = random.Next(100, 999);  // 3-digit random number

            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");

            // Get first 4 characters from SedId
            string firstFourChars = SedId.Length >= 4 ? SedId.Substring(0, 4) : SedId;  

            // Get last 4 characters from RecId
            string lastFourChars = RecId.Length >= 4 ? RecId.Substring(RecId.Length - 4) : RecId;  

             string combinedId = $"{randomNumber}{firstFourChars}{lastFourChars}";

            string shuffledId = new string(combinedId.OrderBy(c => random.Next()).ToArray());

            string REQID = $"TXN-{datePart}-{shuffledId}";

            return REQID;
        }

    }
}
