using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RegistrosCTe.Domain.Entities
{
    public class Viagem:BaseEntity
    {
        public Viagem(string origem, string destino, decimal distancia, DateTime dataInicio):base()
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            DataInicio = dataInicio;
        }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(10,ErrorMessage ="O tamanho minimo de 10 caracteres")]
        public string Origem { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [MinLength(10,ErrorMessage ="O tamanho minimo de 10 caracteres")]
        public string Destino { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas valores positivos")]
        public decimal Distancia { get; set; }
        
        [Required(ErrorMessage = "campo Obrigatorio")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "campo Obrigatorio")]
        public decimal ValorFrete { get; set; }
        public int CargaId { get; set; }
        public virtual Carga Carga { get; set; }
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
            decimal DespesasAdicionais = DespesaAdicionais.Sum(d => d.Valor);
            ValorFrete = (100 * Carga.Peso) + DespesasAdicionais; 
        }
    }
}
