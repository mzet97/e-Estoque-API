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
    }
}
