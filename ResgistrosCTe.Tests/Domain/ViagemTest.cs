using RegistrosCTe.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResgistrosCTe.Tests.Domain
{
    public class ViagemTests
    {
        [Fact]
        public void Viagem_CriarViagemValida_DeveCriarComSucesso()
        {
            // Arrange
            string origem = "São Paulo";
            string destino = "Rio de Janeiro";
            decimal distancia = 450.00m;
            DateTime dataInicio = DateTime.Now;
            int cargaId = 1;

            // Act
            var viagem = new Viagem(origem, destino, distancia, dataInicio, cargaId);

            // Assert
            Assert.Equal(origem, viagem.Origem);
            Assert.Equal(destino, viagem.Destino);
            Assert.Equal(distancia, viagem.Distancia);
            Assert.Equal(dataInicio, viagem.DataInicio);
            Assert.Equal(cargaId, viagem.CargaId);
        }

        [Theory]
        [InlineData("SP", "Rio de Janeiro", 450, "2025-03-04T01:42:46.956Z", 1)] 
        [InlineData("São Paulo", "RJ", 450, "2025-03-04T01:42:46.956Z", 1)] 
        [InlineData("São Paulo", "Rio de Janeiro", -1, "2025-03-04T01:42:46.956Z", 1)]
        public void Viagem_CriarViagemComDadosInvalidos_DeveInvalidarDataAnnotations(string origem, string destino, decimal distancia, string dataInicio, int cargaId)
        {
            // Arrange
            var viagem = new Viagem(origem, destino, distancia, DateTime.Parse(dataInicio), cargaId);
            var validationContext = new ValidationContext(viagem, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(viagem, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void Viagem_Update_DeveAtualizarPropriedades()
        {
            // Arrange
            var viagemOriginal = new Viagem("São Paulo", "Rio de Janeiro", 450.00m, DateTime.Now, 1);
            var viagemAtualizada = new Viagem("Campinas", "Belo Horizonte", 600.00m, DateTime.Now.AddDays(1), 2);

            // Act
            viagemOriginal.Update(viagemAtualizada);

            // Assert
            Assert.Equal(viagemAtualizada.Origem, viagemOriginal.Origem);
            Assert.Equal(viagemAtualizada.Destino, viagemOriginal.Destino);
            Assert.Equal(viagemAtualizada.Distancia, viagemOriginal.Distancia);
            Assert.Equal(viagemAtualizada.DataInicio, viagemOriginal.DataInicio);
        }

        [Fact]
        public void Viagem_CalculaValorFrete_DeveCalcularCorretamenteComDespesasAdicionais()
        {
            // Arrange
            var viagem = new Viagem("São Paulo", "Rio de Janeiro", 450.00m, DateTime.Now, 1);
            viagem.Carga = new Carga(10, 100.00m, 5.00m);
            viagem.DespesaAdicionais = new List<DespesaAdicional>
            {
                new DespesaAdicional("Pedágio", "Pedágio da Rodovia", 25.00m, 1),
                new DespesaAdicional("Alimentação", "Alimentação do Motorista", 75.00m, 1)
            };

            // Act
            viagem.CalculaValorFrete();

            // Assert
            Assert.Equal(10100.00m, viagem.ValorFrete);
        }

        [Fact]
        public void Viagem_CalculaValorFrete_DeveCalcularCorretamenteSemDespesasAdicionais()
        {
            // Arrange
            var viagem = new Viagem("São Paulo", "Rio de Janeiro", 450.00m, DateTime.Now, 1);
            viagem.Carga = new Carga(10, 100.00m, 5.00m);
            viagem.DespesaAdicionais = new List<DespesaAdicional>();

            // Act
            viagem.CalculaValorFrete();

            // Assert
            Assert.Equal(10000.00m, viagem.ValorFrete);
        }

        [Fact]
        public void Viagem_CalculaValorFreteComPeso_DeveCalcularCorretamente()
        {
            // Arrange
            var viagem = new Viagem("São Paulo", "Rio de Janeiro", 450.00m, DateTime.Now, 1);

            // Act
            viagem.CalculaValorFrete(150.00m);

            // Assert
            Assert.Equal(15000.00m, viagem.ValorFrete);
        }
    }
}