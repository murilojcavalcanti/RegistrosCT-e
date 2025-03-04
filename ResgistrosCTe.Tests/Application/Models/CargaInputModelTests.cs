using RegistrosCTe.Application.Models.CargaModelss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResgistrosCTe.Tests.Application.Models
{
    public class CargaInputModelTests
    {
        [Theory]
        [InlineData(1, 10.5, 5.0)]
        [InlineData(100, 50.0, 25.5)]
        public void CargaInputModel_DadosValidos_DevePassarValidacao(int quantidade, decimal peso, decimal volume)
        {
            // Arrange
            CargaInputModel model = new CargaInputModel(quantidade, peso, volume);
            var validationContext = new ValidationContext(model,null,null);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(model,validationContext,validationResults,true);

            // Assert
            Assert.Empty(validationResults);
        }

        [Theory]
        [InlineData(-1, 10.5, 5.0)]
        [InlineData(1, -10.5, 5.0)]
        [InlineData(1, 10.5, -5.0)]
        [InlineData(-1, -10.5, -5.0)]
        public void CargaInputModel_DadosInvalidos_DeveFalharValidacao(int quantidade, decimal peso, decimal volume)
        {
            // Arrange
            var model = new CargaInputModel(quantidade, peso, volume);

            // Act
            var validationResults = ValidarModelo(model);

            // Assert
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void ParaEntidade_DeveRetornarInstanciaDeCarga()
        {
            // Arrange
            var model = new CargaInputModel(10, 20.5m, 15.3m);

            // Act
            var entity = model.ToEntity();

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(model.Quantidade, entity.Quantidade);
            Assert.Equal(model.Peso, entity.Peso);
            Assert.Equal(model.Volume, entity.Volume);
        }

        private List<ValidationResult> ValidarModelo(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
