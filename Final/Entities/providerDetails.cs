namespace Final.Entities
{
    public class providerDetails
    {
        public  Guid providerId { get; set; }
        public string providerName { get; set; }
        public string providerEmail { get; set; }
        public string providerMobile { get; set; }
        public string providerPassword  { get; set; }
        public serivcesList[] providerService { get; set; }
        public string location { get; set; }
        public string description { get; set; }

    }
}
