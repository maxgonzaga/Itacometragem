using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Itacometragem.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Insira o nome de usuário.")]
        [StringLength(20, ErrorMessage = "O nome de usuário não pode ser maior que 20 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Insira a senha.")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
