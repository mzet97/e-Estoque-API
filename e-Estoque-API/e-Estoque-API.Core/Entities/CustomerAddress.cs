namespace e_Estoque_API.Core.Entities
{
    public class CustomerAddress : Address
    {
        public CustomerAddress()
        {
        }

        public CustomerAddress(
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
            : base(street, number, complement, neighborhood, district, city, county, zipCode, latitude, longitude)
        {
        }
    }
}
