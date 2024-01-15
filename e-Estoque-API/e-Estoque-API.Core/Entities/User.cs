using Microsoft.AspNetCore.Identity;

namespace e_Estoque_API.Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        #region EFCRelations
        public Guid IdProfileUser { get; set; }
        public virtual ProfileUser ProfileUser { get; set; }
        #endregion

        public User()
        {

        }
    }
}
