using e_Estoque_API.Core.Events.Companies;

namespace e_Estoque_API.Core.Entities
{
    public class Company : AggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string DocId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Guid IdCompanyAddress { get; set; }
        public virtual CompanyAddress CompanyAddress { get; set; } = null!;

        public Company()
        {
        }

        public Company(Guid id, string name, string docId, string email, string description, string phoneNumber, Guid idCompanyAddress, CompanyAddress companyAddress)
        {
            Id= id;
            Name = name;
            DocId = docId;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            IdCompanyAddress = idCompanyAddress;
            CompanyAddress = companyAddress;
        }

       public static Company Create(string name, string docId, string email, string description, string phoneNumber, Guid idCompanyAddress, CompanyAddress companyAddress)
        {
            var company = new Company(Guid.NewGuid(), name, docId, email, description, phoneNumber, idCompanyAddress, companyAddress);

            company.CreatedAt = DateTime.UtcNow;

            company.AddEvent(new CompanyCreated(company.Id, company.Name, company.DocId, company.Email, company.Description, company.PhoneNumber, company.IdCompanyAddress, company.CompanyAddress));

            return company;
        }

        public void Update(string name, string docId, string email, string description, string phoneNumber, Guid idCompanyAddress, CompanyAddress companyAddress)
        {
            Name = name;
            DocId = docId;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            IdCompanyAddress = idCompanyAddress;
            CompanyAddress = companyAddress;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new CompanyUpdated(Id, Name, DocId, Email, Description, PhoneNumber, IdCompanyAddress, CompanyAddress));
        }
    }
}
