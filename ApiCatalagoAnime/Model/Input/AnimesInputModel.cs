using System;
using System.ComponentModel.DataAnnotations;


namespace ApiCatalagoAnime.Model.Input
{
    public class AnimesInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do anime deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 3 e 100 caracteres")]
        public string Produtora { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do Gênero deve conter entre 3 e 100 caracteres")]
        public string Genero { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public double Preco { get; set; }
    }
}
