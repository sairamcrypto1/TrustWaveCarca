namespace TrustWaveCarca.Data
{
    public class ChatRequest
    {
        public int id { get; set; }
        public string Sender_UniqueId { get; set; }
        public string SenderEmail { get; set; }
        public DateOnly RequestDate { get; set; }
        public string ReceiverEmail { get; set; }
        public string Receiver_UniqueId { get; set; }
        public DateOnly AcceptDate { get; set; }
        public string RequestId { get; set; }
        public bool IsChatActive { get; set; } = false;      
        public string Status { get; set; } = "Pending";
    }
}
