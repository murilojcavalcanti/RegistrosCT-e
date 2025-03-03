using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class Carga:BaseEntity
    {
        public Carga(int quantidade, decimal peso, decimal volume)
        {
            Quantidade = quantidade;
            Peso = peso;
            Volume = volume;
        }

        [Required(ErrorMessage ="campo Obrigatorio")]
        [Range(0,int.MaxValue,ErrorMessage ="Apenas valores positivos")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage ="campo Obrigatorio")]
        [Column(TypeName = "decimal(10,3)")]
        [Range(0,int.MaxValue,ErrorMessage ="Apenas valores positivos")]
        public decimal Peso { get; set; }
        
        [Required(ErrorMessage ="campo Obrigatorio")]
        [Column(TypeName = "decimal(10,3)")]
        [Range(0,int.MaxValue,ErrorMessage ="Apenas valores positivos")]
        public decimal Volume { get; set; }
        public virtual Viagem Viagem { get; set; }
        public void Update(Carga carga)
        {
            Peso = carga.Peso;
            Quantidade = carga.Quantidade;
            Volume = carga.Volume;
        }
    }
}
