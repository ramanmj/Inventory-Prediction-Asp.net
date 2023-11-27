using System.ComponentModel.DataAnnotations;

namespace InventoryManagement_api.Models.Design
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string gender { get; set; }
        public string Phone { get; set; }
    }
}
