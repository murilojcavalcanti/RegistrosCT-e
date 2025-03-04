using RegistrosCTe.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResgistrosCTe.Tests.Domain
{
    public class CargaTests
    {
        [Fact]
        public void Carga_CriarCargaValida_DeveCriarComSucesso()
        {
            // Arrange
            int quantidade = 10;
            decimal peso = 100.500m;
            decimal volume = 5.200m;

            // Act
            var carga = new Carga(quantidade, peso, volume);

            // Assert
            Assert.Equal(quantidade, carga.Quantidade);
            Assert.Equal(peso, carga.Peso);
            Assert.Equal(volume, carga.Volume);
        }

        [Theory]
        [InlineData(-1, 100, 5)]
        [InlineData(10, -1, 5)]
        [InlineData(10, 100, -1)]
        public void Carga_CriarCargaComValoresNegativos_DeveInvalidarDataAnnotations(int quantidade, decimal peso, decimal volume)
        {
            // Arrange
            var carga = new Carga(quantidade, peso, volume);
            var validationContext = new ValidationContext(carga, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(carga, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void Carga_Update_DeveAtualizarPropriedades()
        {
            // Arrange
            var cargaOriginal = new Carga(10, 100.500m, 5.200m);
            var cargaAtualizada = new Carga(20, 200.750m, 10.500m);

            // Act
            cargaOriginal.Update(cargaAtualizada);

            // Assert
            Assert.Equal(cargaAtualizada.Quantidade, cargaOriginal.Quantidade);
            Assert.Equal(cargaAtualizada.Peso, cargaOriginal.Peso);
            Assert.Equal(cargaAtualizada.Volume, cargaOriginal.Volume);
        }
    }
}
