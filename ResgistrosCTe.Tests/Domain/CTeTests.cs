using RegistrosCTe.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResgistrosCTe.Tests.Domain
{
    public class CTeTests
    {
        [Fact]
        public void CTe_CriarCTeValido_DeveCriarComSucesso()
        {
            // Arrange
            decimal aliquota = 18.00m;
            int viagemId = 1;
            
            // Act
            CTe cte = new(aliquota, viagemId);

            // Assert
            Assert.Equal(aliquota, cte.Aliquota);
            Assert.Equal(viagemId, cte.ViagemId);
            Assert.Equal(DateTime.Now.Date, cte.DataEmissao.Date);

        }
        [Fact]
        public void CTe_CalculaValorBaseSimples_DeveCalcularCorretamente()
        {
            // Arrange
            decimal aliquota = 18.00m;
            int viagemId = 1;

            CTe cte = new(aliquota, viagemId);
            cte.Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1);
            cte.Viagem.ValorFrete = 1000;

            // Act
            cte.CalculaValorBaseSimples();

            // Assert
            Assert.Equal(180.00m, cte.ValorICMS);
            Assert.Equal(1180.00m, cte.ValorCTe);
        }

        [Fact]
        public void CTe_CalculaValorBasePorDentro_DeveCalcularCorretamente()
        {
            // Arrange
            decimal aliquota = 18.00m;
            int viagemId = 1;

            CTe cte = new(aliquota, viagemId);
            cte.Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1);
            cte.Viagem.ValorFrete = 1000;

            // Act
            cte.CalculaValorBasePorDentro();

            // Assert
            Assert.Equal(219.51m, Math.Round(cte.ValorICMS, 2));
            Assert.Equal(1219.51m, Math.Round(cte.ValorCTe, 2));
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData(18, null)]
        public void CTe_CriarCTeComValoresInvalidos_DeveInvalidarDataAnnotations(decimal aliquota, int viagemId)
        {
            // Arrange
            var cte = new CTe(aliquota, viagemId);
            var validationContext = new ValidationContext(cte, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(cte, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }
    }
}
