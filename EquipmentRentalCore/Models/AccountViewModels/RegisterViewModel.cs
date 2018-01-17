using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane jest podanie nazwy użytkownika!")]
        public string Login { get; set; }
        [Display(Name = "Hasło"), StringLength(20)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane jest podanie hasła!")]
        public string Password { get; set; }
        [Display(Name = "Potwierdzenie hasła"), StringLength(20)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane jest potwierdzenie hasła!")]
        public string ConfirmPassword { get; set; }
    }
}
