namespace e_Estoque_API.Core.Entities
{
    public class Address : AggregateRoot
    {
        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;
        public string Complement { get; private set; } = string.Empty;
        public string Neighborhood { get; private set; } = string.Empty;
        public string District { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string County { get; private set; } = string.Empty;
        public string ZipCode { get; private set; } = string.Empty;
        public string Latitude { get; private set; } = string.Empty;
        public string Longitude { get; private set; } = string.Empty;

        public Address()
        {
            
        }

        public Address(
            string street,
            string number,
            string complement,
            string neighborhood,
            string district,
            string city,
            string county,
            string zipCode,
            string latitude,
            string longitude)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            District = district;
            City = city;
            County = county;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
