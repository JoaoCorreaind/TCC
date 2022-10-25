namespace WebApplication1.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string CityId { get; set; }
        public City City { get; set; }
        public string ZipCode { get; set; }
        public string PublicPlace { get; set; }
        public string District { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ReferencePoint { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
