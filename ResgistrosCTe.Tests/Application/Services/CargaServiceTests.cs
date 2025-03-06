using Moq;
using RegistrosCTe.Application.Models.CargaModels;
using RegistrosCTe.Application.Models.CargaModelss;
using RegistrosCTe.Application.Services.CargaServices;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CargaRepositories;

namespace ResgistrosCTe.Tests.Application.Services
{
    public class CargaServiceTests
    {
        [Fact]
        public async Task Post_CargaInputModelValido_DeveRetornarCargaViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargaInputModel = new CargaInputModel (10,100,5);
            var carga = new Carga(cargaInputModel.Quantidade, cargaInputModel.Peso, cargaInputModel.Volume) { Id = 1 };

            mockRepository.Setup(repo => repo.Post(It.IsAny<Carga>())).ReturnsAsync(carga);
            var service = new CargaService(mockRepository.Object);

            // Act
            var result = await service.Post(cargaInputModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CargaViewModel>(result);
            mockRepository.Verify(repo => repo.Post(It.IsAny<Carga>()), Times.Once);
        }

        [Fact]
        public async Task Post_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargaInputModel = new CargaInputModel(10, 100, 5) ;
            mockRepository.Setup(repo => repo.Post(It.IsAny<Carga>())).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Post(cargaInputModel));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.Post(It.IsAny<Carga>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDeCargaViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargas = new List<Carga>
            {
                new Carga(10, 100, 5) { Id = 1 },
                new Carga(20, 200, 10) { Id = 2 }
            };

            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(cargas);
            var service = new CargaService(mockRepository.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CargaViewModel>>(result);
            Assert.Equal(2, result.Count);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            mockRepository.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetAll());
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_IdValido_DeveRetornarCargaViewModelDetails()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var carga = new Carga(10, 100, 5) { Id = 1 };
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(carga);
            var service = new CargaService(mockRepository.Object);

            // Act
            var result = await service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CargaViewModelDetails>(result);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CargaExiste_DeveChamarUpdateNoRepositorio()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargaInputModel = new CargaInputModel(10, 100, 5);
            var carga = new Carga(10, 100, 5) { Id = 1 };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(carga);
            mockRepository.Setup(repo => repo.Update(It.IsAny<Carga>()));

            var service = new CargaService(mockRepository.Object);

            // Act
            service.Update(1, cargaInputModel);

            // Assert
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<Carga>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CargaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargaInputModel = new CargaInputModel (20,200,10);
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Carga)null);

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Update(1, cargaInputModel));
            Assert.Equal("Erro no processamento:Carga não encontrada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<Carga>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_CargaComViagem_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var cargaInputModel = new CargaInputModel(20, 200, 10);
            var carga = new Carga(10, 100, 5) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(carga);

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Update(1, cargaInputModel));
            Assert.Equal("Erro no processamento:Carga não pode ser atualizada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<Carga>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_CargaExiste_DeveChamarDeleteNoRepositorio()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var carga = new Carga(10, 100, 5) { Id = 1 };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(carga);
            mockRepository.Setup(repo => repo.Delete(It.IsAny<Carga>()));

            var service = new CargaService(mockRepository.Object);

            // Act
            service.Delete(1);

            // Assert
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Carga>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CargaComViagem_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            var carga = new Carga(10, 100, 5) { Id = 1, Viagem = new Viagem("Origem", "Destino", 100, DateTime.Now, 1) };

            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync(carga);

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception =await Assert.ThrowsAsync<Exception>(()=>service.Delete(1));
            Assert.Equal("Erro no processamento:Carga não pode ser Deletada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Carga>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_CargaNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICargaRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).ReturnsAsync((Carga)null); 

            var service = new CargaService(mockRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Delete(1));
            Assert.Equal("Erro no processamento:Carga não encontrada!!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(It.IsAny<Carga>()), Times.Never);
        }
    }
}
