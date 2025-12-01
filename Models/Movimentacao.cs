using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROVASAEP.Models
{
    public class Movimentacao
    {
        public int Id { get; set; }

        [Required]
        public int MaterialId { get; set; }

        public Material Material { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; } // Entrada / Saída

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        [Required]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}
