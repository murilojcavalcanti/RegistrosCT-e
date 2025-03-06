using Moq;
using RegistrosCTe.Application.Services.DespesasServices;
using RegistrosCTe.Application.Services.ViagemService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.DespesasAdicionaisRepositories;
using RegistrosDespesaAdicional.Application.Models.DespesaAdicionalModels;

namespace ResgistrosCTe.Tests.Application.Services
{
    public class DesapesaServiceTests
    {
        [Fact]
        public async Task Post_DespesaAdicionalInputModelValido_DeveRetornarDespesaAdicionalViewModel()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesaInputModel = new DespesaAdicionalInputModel ("Teste","Descricao",100,1);
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1 };

            mockRepository.Setup(repo => repo.Post(It.IsAny<DespesaAdicional>())).ReturnsAsync(despesa);
            mockViagemService.Setup(service => service.CalculaValorFrete(1));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act
            var result = await service.Post(despesaInputModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaAdicionalViewModel>(result);
            mockRepository.Verify(repo => repo.Post(It.IsAny<DespesaAdicional>()), Times.Once);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Once);
        }

        [Fact]
        public async Task Post_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesaInputModel = new DespesaAdicionalInputModel("Teste", "Descricao", 100, 1);
            mockRepository.Setup(repo => repo.Post(It.IsAny<DespesaAdicional>())).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Post(despesaInputModel));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.Post(It.IsAny<DespesaAdicional>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeDespesaAdicionalViewModel()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesas = new List<DespesaAdicional>
            {
                new DespesaAdicional("Teste1", "Descricao1", 100, 1),
                new DespesaAdicional("Teste2", "Descricao2", 200, 2)
            };

            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(despesas);
            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<DespesaAdicionalViewModel>>(result);
            Assert.Equal(2, result.Count);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            mockRepository.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetAll());
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetById_IdValido_DeveRetornarDespesaAdicionalViewModelDetails()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(despesa);

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act
            var result = await service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DespesaAdicionalViewModelDetails>(result);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public async Task GetById_RepositoryRetornaNull_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((DespesaAdicional)null);

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Despesa não encontrada!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public async Task GetById_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            mockRepository.Setup(repo => repo.GetById(1)).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public async Task Update_DespesaExiste_DeveChamarUpdateNoRepositorioECalculaValorFrete()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>(); 
            var despesaUpdateInputModel = new DespesaAdicionalUpdateInputModel("TesteAtualizado", "DescricaoAtualizada", 200 );
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(despesa);
            mockRepository.Setup(repo => repo.Update(It.IsAny<DespesaAdicional>()));
            mockViagemService.Setup(service => service.CalculaValorFrete(1));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act
            service.Update(1, despesaUpdateInputModel);

            // Assert
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<DespesaAdicional>()), Times.Once);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Once);
        }

        [Fact]
        public async Task Update_DespesaComCTe_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesaUpdateInputModel = new DespesaAdicionalUpdateInputModel("TesteAtualizado", "DescricaoAtualizada", 200);
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) { CTe = new CTe(10, 1) } };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(despesa);
            mockRepository.Setup(repo => repo.Update(It.IsAny<DespesaAdicional>()));
            mockViagemService.Setup(service => service.CalculaValorFrete(1));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => service.Update(1, despesaUpdateInputModel));

            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<DespesaAdicional>()), Times.Never);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Never);
        }

        [Fact]
        public async Task Update_DespesaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesaUpdateInputModel = new DespesaAdicionalUpdateInputModel("TesteAtualizado", "DescricaoAtualizada", 200);
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((DespesaAdicional)null);

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await service.Update(1, despesaUpdateInputModel));
            Assert.Equal("Erro no processamento:Despesa não encontrada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<DespesaAdicional>()), Times.Never);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Never);
        }

        [Fact]
        public async Task Delete_DespesaExiste_DeveChamarDeleteNoRepositorioECalculaValorFrete()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(despesa);
            mockRepository.Setup(repo => repo.Delete(It.IsAny<DespesaAdicional>()));
            mockViagemService.Setup(service => service.CalculaValorFrete(1));

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act
            service.Delete(1);

            // Assert
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<DespesaAdicional>()), Times.Once);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Once);
        }

        [Fact]
        public async Task Delete_DespesaComCTe_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            var despesa = new DespesaAdicional("Teste", "Descricao", 100, 1) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) { CTe = new CTe(10, 1) } };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(despesa);

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () =>await service.Delete(1));

            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<DespesaAdicional>()), Times.Never);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Never);
        }

        [Fact]
        public async Task Delete_DespesaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<IDespesasRepository>();
            var mockViagemService = new Mock<IViagemService>();
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((DespesaAdicional)null);

            var service = new DespesasService(mockRepository.Object, mockViagemService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await service.Delete(1));
            Assert.Equal("Erro no processamento:Despesa não encontrada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<DespesaAdicional>()), Times.Never);
            mockViagemService.Verify(service => service.CalculaValorFrete(1), Times.Never);
        }
    }
}
