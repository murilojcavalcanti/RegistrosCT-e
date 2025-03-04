using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistrosCTe.Domain.Entities
{
    public class Viagem:BaseEntity
    {
        public Viagem(string origem, string destino, decimal distancia, DateTime dataInicio, int cargaId) : base()
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            DataInicio = dataInicio;
            CargaId = cargaId;
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(5,ErrorMessage ="O tamanho minimo de 5 caracteres")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(5,ErrorMessage ="O tamanho minimo de 5 caracteres")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Column(TypeName = "decimal(10,3)")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Distancia { get; set; }
        
        [Required(ErrorMessage = "campo Obrigatorio")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorFrete { get; set; }
        public int CargaId { get; set; }
        public virtual Carga Carga { get; set; }
        public virtual CTe CTe { get; set; }
        public List<DespesaAdicional> DespesaAdicionais { get; set; }

        public void Update(Viagem viagem)
        {
            Origem = viagem.Origem;
            Destino = viagem.Destino;
            Distancia =  viagem.Distancia;
            DataInicio = viagem.DataInicio;
        }

        public void CalculaValorFrete()
        {
            decimal DespesasAdicionais = DespesaAdicionais.Count() > 0 ? DespesaAdicionais.Sum(d => d.Valor) : 0;
            ValorFrete = (100 * Carga.Peso) + DespesasAdicionais; 
        }
        public void CalculaValorFrete(decimal Peso)
        {
            ValorFrete = 100 * Peso;
        }
    }
}
