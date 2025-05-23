﻿namespace e_Estoque_API.Domain.ValueObjects;

public sealed record CompanyAddress(
     string Street,
     string Number,
     string Complement,
     string Neighborhood,
     string District,
     string City,
     string Country,
     string ZipCode,
     string Latitude,
     string Longitude
 );
