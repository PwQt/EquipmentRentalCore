using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentRentalCore.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane jest podanie nazwy użytkownika")]
        [StringLength(20, ErrorMessage = "Za długa nazwa użytkownika")]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane jest podanie hasła")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Nieprawidłowa długość hasła")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj logowanie")]
        public bool RememberLogin { get; set; }
    }
}
