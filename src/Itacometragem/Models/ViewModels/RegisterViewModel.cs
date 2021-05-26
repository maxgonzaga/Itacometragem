using System.ComponentModel.DataAnnotations;

namespace Itacometragem.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Insira um nome de usuário.")]
        [StringLength(20, ErrorMessage = "O nome de usuário não pode ser maior que 20 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Insira a senha.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme a senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string ConfirmPassword { get; set; }
    }
}
