using RegistrosCTe.Domain.Entities;

namespace RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels
{
    public class DespesaAdicionalViewModel
    {
        public DespesaAdicionalViewModel(string nome, string descricao, decimal valor, int despesaId)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            DespesaId = despesaId;
        }

        public int DespesaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public static DespesaAdicionalViewModel FromEntity(DespesaAdicional DespesaAdicional)
            => new(DespesaAdicional.Nome, DespesaAdicional.Descricao, DespesaAdicional.Valor,DespesaAdicional.Id);

    }
}
