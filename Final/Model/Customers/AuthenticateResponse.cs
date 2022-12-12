namespace Final.Model.Customers
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string Token { get; set; }
    }
}
