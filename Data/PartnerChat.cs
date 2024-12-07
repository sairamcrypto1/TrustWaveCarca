namespace TrustWaveCarca.Data
{
    public class PartnerChat
    {
        public int id { get; set; }
        public string Sender_UniqueId { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Receiver_UniqueId { get; set; }
        public DateOnly AcceptDate { get; set; }
        public string Status { get; set; }
        public bool Isdelete { get; set; }=false;
        public DateOnly DeleteDate { get; set; }
        public bool block { get; set; } = false;
        public DateTime LastUpdated { get; set; }

    }
}
