using RegistrosCTe.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResgistrosCTe.Tests.Domain
{
    public class DespesaAdicionalTests
    {
        [Fact]
        public void DespesaAdicional_CriarDespesaValida_DeveCriarComSucesso()
        {
            // Arrange
            string nome = "Alimentação";
            string descricao = "Alimentação do motorista";
            decimal valor = 50.00m;
            int viagemId = 1;

            // Act
            var despesa = new DespesaAdicional(nome, descricao, valor, viagemId);

            // Assert
            Assert.Equal(nome, despesa.Nome);
            Assert.Equal(descricao, despesa.Descricao);
            Assert.Equal(valor, despesa.Valor);
            Assert.Equal(viagemId, despesa.ViagemId);
        }

        [Theory]
        [InlineData("Ali", "Descricao", 50, 1)] 
        [InlineData("Nome", "Desc", 50, 1)] 
        [InlineData("Nome", "Descricao", -1, 1)] 
        public void DespesaAdicional_CriarDespesaComDadosInvalidos_DeveInvalidarDataAnnotations(string nome, string descricao, decimal valor, int viagemId)
        {
            // Arrange
            var despesa = new DespesaAdicional(nome, descricao, valor, viagemId);
            var validationContext = new ValidationContext(despesa, null, null);
            var validationResults = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(despesa, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.NotEmpty(validationResults);
        }

        [Fact]
        public void DespesaAdicional_Update_DeveAtualizarPropriedades()
        {
            // Arrange
            var despesaOriginal = new DespesaAdicional("Alimentação", "Alimentação do motorista", 50.00m, 1);
            var despesaAtualizada = new DespesaAdicional("Hospedagem", "Hospedagem do motorista", 100.00m, 1);

            // Act
            despesaOriginal.Update(despesaAtualizada);

            // Assert
            Assert.Equal(despesaAtualizada.Nome, despesaOriginal.Nome);
            Assert.Equal(despesaAtualizada.Descricao, despesaOriginal.Descricao);
            Assert.Equal(despesaAtualizada.Valor, despesaOriginal.Valor);
        }
    }
}
