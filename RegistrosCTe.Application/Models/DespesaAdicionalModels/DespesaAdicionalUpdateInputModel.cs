using RegistrosCTe.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels
{
    public class DespesaAdicionalUpdateInputModel
    {
        public DespesaAdicionalUpdateInputModel(string nome, string descricao, decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4, ErrorMessage = "Quantidade minima de 4 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(4, ErrorMessage = "Quantidade minima de 4 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Valor { get; set; }
        public DespesaAdicional ToEntity() => new DespesaAdicional(Nome, Descricao,Valor);
    }
}
