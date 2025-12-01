using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PROVASAEP.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Tipo { get; set; }

        public string Cor { get; set; }

        public string Textura { get; set; }

        public string UnidadeDeMedida { get; set; }

        public int QuantidadeEstoque { get; set; }

        public DateTime? PrazoDeValidade { get; set; }

        public string Aplicacao { get; set; }

        public string Fabricacao { get; set; }
    }
}