using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class DespesaAdicional:BaseEntity
    {
        public DespesaAdicional(string nome, string descricao, decimal valor, int viagemId)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            ViagemId = viagemId;
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4,ErrorMessage ="Quantidade minima de 4 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4,ErrorMessage ="Quantidade minima de 4 caracteres")]
        public string Descricao { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Valor { get; set; }
        public int ViagemId { get; set; }
        public virtual Viagem Viagem { get; set; }
        public void Update(DespesaAdicional despesaAdicional)
        {
            Nome = despesaAdicional.Nome;
            Descricao = despesaAdicional.Descricao;
            Valor = despesaAdicional.Valor;
        }
    }
}
