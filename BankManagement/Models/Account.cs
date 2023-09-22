using BankManagement.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Balance cannot be negative")]
        public int InitialBalance { get; set; }

        public string BankUserId { get; set; }
        public BankUser? BankUser { get; set; }
    }
}
