namespace e_Estoque_API.Core.Entities
{
    public class ProfileUser : AggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public int Type { get; set; }

        public ProfileUser()
        {

        }

        public ProfileUser(string name, string lastName, DateTime? birthDate, int type)
        {
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Type = type;
        }


    }
}
