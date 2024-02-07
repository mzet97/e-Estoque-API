using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Application.Dtos.ViewModels;

public class AddressViewModel : BaseViewModel
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string ZipCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }

    public AddressViewModel(
       Guid id,
       string street,
       string number,
       string complement,
       string neighborhood,
       string district,
       string city,
       string county,
       string zipCode,
       string latitude,
       string longitude,
       DateTime createdAt,
       DateTime? updatedAt,
       DateTime? deletedAt
       ) : base(id, createdAt, updatedAt, deletedAt)
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

    public static AddressViewModel FromEntity(Address entity)
    {
        return new AddressViewModel(
            entity.Id,
            entity.Street,
            entity.Number,
            entity.Complement,
            entity.Neighborhood,
            entity.District,
            entity.City,
            entity.County,
            entity.ZipCode,
            entity.Latitude,
            entity.Longitude,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}