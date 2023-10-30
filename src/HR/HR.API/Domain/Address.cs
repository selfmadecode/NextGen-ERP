namespace HR.API.Domain
{
    public class Address
    {
        public required string HomeAddress { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public string Country { get; set; }
    }
}
