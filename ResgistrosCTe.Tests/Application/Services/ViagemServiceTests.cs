using Moq;
using RegistrosCTe.Application.Models.ViagemModels;
using RegistrosCTe.Application.Services.CTeService;
using RegistrosCTe.Application.Services.ViagemService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.ViagemRepositories;

namespace ResgistrosCTe.Tests.Application.Services
{
    public class ViagemServiceTests
    {

        [Fact]
        public void Post_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagemInputModel = new ViagemInputModel("São Paulo,São Paulo", "Rio De Janeiro,Rio De Janeiro", 400, DateTime.Now, 1);
            mockViagemRepository.Setup(repo => repo.Post(It.IsAny<Viagem>())).Throws(new Exception("Erro no repositório"));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Post(viagemInputModel));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockViagemRepository.Verify(repo => repo.Post(It.IsAny<Viagem>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeViagemViewModel()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagens = new List<Viagem>
            {
                new Viagem("SP", "RJ", 400, DateTime.Now, 1),
                new Viagem("MG", "ES", 600, DateTime.Now, 2)
            };

            mockViagemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(viagens);
            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ViagemViewModel>>(result);
            Assert.Equal(2, result.Count);
            mockViagemRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_RepositoryRetornaNull_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();

            mockViagemRepository.Setup(repo => repo.GetAll()).ReturnsAsync((List<Viagem>)null); // Simula retorno null
            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetAll());
            Assert.Equal("Erro no processamento:Viagens não existem!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            mockViagemRepository.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Erro no repositório"));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => service.GetAll());
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_IdValido_DeveRetornarViagemViewModelDetails()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagem = new Viagem("SP", "RJ", 400, DateTime.Now, 1) { Id = 1 };
            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns(viagem);

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViagemViewModelDetails>(result);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void GetById_RepositoryRetornaNull_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();

            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns((Viagem)null); // Simula retorno null
            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Viagem não existe!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void GetById_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            mockViagemRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void Update_ViagemExiste_DeveChamarUpdateNoRepositorio()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagemUpdateInputModel = new ViagemUpdateInputModel( "São Paulo,São Paulo", "Rio De Janeiro,Rio De Janeiro", 400, DateTime.Now);
            var viagem = new Viagem("São Paulo,São Paulo", "Rio De Janeiro,Rio De Janeiro", 400, DateTime.Now, 1) { Id = 1 }; //Simula uma viagem

            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns(viagem);
            mockViagemRepository.Setup(repo => repo.Update(It.IsAny<Viagem>()));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act
            service.Update(1, viagemUpdateInputModel);

            // Assert
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockViagemRepository.Verify(repo => repo.Update(It.IsAny<Viagem>()), Times.Once);
        }

        [Fact]
        public void Update_ViagemNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagemUpdateInputModel = new ViagemUpdateInputModel("São Paulo,São Paulo", "Rio De Janeiro,Rio De Janeiro", 400, DateTime.Now);
            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns((Viagem)null);

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Update(1, viagemUpdateInputModel));
            Assert.Equal("Erro no processamento:Viagem não encontrada!!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockViagemRepository.Verify(repo => repo.Update(It.IsAny<Viagem>()), Times.Never);
        }

        [Fact]
        public void Update_ViagemComCTe_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagemUpdateInputModel = new ViagemUpdateInputModel("São Paulo,São Paulo", "Rio De Janeiro,Rio De Janeiro", 400, DateTime.Now);
            var viagem = new Viagem("SP", "RJ", 400, DateTime.Now, 1) { Id = 1, CTe = new CTe(10, 1) };

            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns(viagem);

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Update(1, viagemUpdateInputModel));
            Assert.Equal("Erro no processamento:Viagem não pode ser Atualizada!!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockViagemRepository.Verify(repo => repo.Update(It.IsAny<Viagem>()), Times.Never);
        }

        [Fact]
        public void Delete_ViagemExiste_DeveChamarDeleteNoRepositorio()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            var viagem = new Viagem("SP", "RJ", 400, DateTime.Now, 1) { Id = 1 };

            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns(viagem);
            mockViagemRepository.Setup(repo => repo.Delete(It.IsAny<Viagem>()));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act
            service.Delete(1);

            // Assert
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockViagemRepository.Verify(repo => repo.Delete(It.IsAny<Viagem>()), Times.Once);
        }

        [Fact]
        public void Delete_ViagemNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();

            mockViagemRepository.Setup(repo => repo.GetById(1)).Returns((Viagem)null);

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Delete(1));
            Assert.Equal("Erro no processamento:Viagem não encontrada!!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockViagemRepository.Verify(repo => repo.Delete(It.IsAny<Viagem>()), Times.Never);
        }

        [Fact]
        public void CalculaValorFrete_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockViagemRepository = new Mock<IViagemRepository>();
            var mockCTeService = new Mock<ICTeService>();
            mockViagemRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new ViagemService(mockViagemRepository.Object, mockCTeService.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.CalculaValorFrete(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockViagemRepository.Verify(repo => repo.GetById(1), Times.Once);
        }
    }
}
