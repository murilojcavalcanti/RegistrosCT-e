using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class DespesaAdicional:BaseEntity
    {
        public DespesaAdicional(string nome, string description, decimal valor)
        {
            Nome = nome;
            Description = description;
            Valor = valor;
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4,ErrorMessage ="Quantidade minima de 4 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4,ErrorMessage ="Quantidade minima de 4 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Valor { get; set; }
        public int ViagemId { get; set; }
        public virtual Viagem Viagem { get; set; }
        public void Update(DespesaAdicional despesaAdicional)
        {
            Nome = despesaAdicional.Nome;
            Description = despesaAdicional.Description;
            Valor = despesaAdicional.Valor;
        }
    }
}
