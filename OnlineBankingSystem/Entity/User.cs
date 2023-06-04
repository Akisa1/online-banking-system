using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBankingSystem.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";

            }
        }

        public DateTime BirthDate { get; set; }
    }
}
