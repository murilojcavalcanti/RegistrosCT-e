using Moq;
using RegistrosCTe.Application.Models.CTeModels;
using RegistrosCTe.Application.Services.CTeService;
using RegistrosCTe.Domain.Entities;
using RegistrosCTe.Infra.Repostories.CTeRepositories;

namespace ResgistrosCTe.Tests.Application.Services
{
    public class CTeServiceTests
    {
        [Fact]
        public void Post_CTeInputModelValido_DeveRetornarCTeViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cteInputModel = new CTeInputModel(10,1);
            var cte = new CTe(cteInputModel.Aliquota, cteInputModel.ViagemId) { Id = 1 };
            cte.Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1) { ValorFrete = 1000 };

            mockRepository.Setup(repo => repo.Post(It.IsAny<CTe>())).Returns(cte);
            mockRepository.Setup(repo => repo.GetById(1)).Returns(cte);
            mockRepository.Setup(repo => repo.Update(It.IsAny<CTe>()));
            var service = new CTeService(mockRepository.Object);

            // Act
            var result = service.Post(cteInputModel);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CTeViewModel>(result);
            mockRepository.Verify(repo => repo.Post(It.IsAny<CTe>()), Times.Once);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(It.IsAny<CTe>()), Times.Once);
        }

        [Fact]
        public void Post_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cteInputModel = new CTeInputModel (10,1);
            mockRepository.Setup(repo => repo.Post(It.IsAny<CTe>())).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Post(cteInputModel));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.Post(It.IsAny<CTe>()), Times.Once);
        }

        [Fact]
        public void GetAll_DeveRetornarListaDeCTeViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var ctes = new List<CTe>
            {
                new CTe(10, 1) { Id = 1, ValorCTe = 100, ValorICMS = 10 },
                new CTe(20, 2) { Id = 2, ValorCTe = 200, ValorICMS = 20 }
            };
            ctes[0].Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1) { ValorFrete = 1000 };
            ctes[1].Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1) { ValorFrete = 2000 };

            mockRepository.Setup(repo => repo.GetAll()).Returns(ctes);
            var service = new CTeService(mockRepository.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CTeViewModel>>(result);
            Assert.Equal(2, result.Count);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.GetAll());
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetById_IdValido_DeveRetornarCTeViewModelDetails()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cte = new CTe(10, 1) { Id = 1, ValorCTe = 100, ValorICMS = 10 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(cte);
            var service = new CTeService(mockRepository.Object);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CTeViewModelDetails>(result);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void GetById_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.GetById(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void Delete_IdValido_DeveChamarDeleteNoRepositorio()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cte = new CTe(10, 1) { Id = 1, ValorCTe = 100, ValorICMS = 10 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(cte);
            mockRepository.Setup(repo => repo.Delete(cte));

            var service = new CTeService(mockRepository.Object);

            // Act
            service.Delete(1);

            // Assert
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Delete(cte), Times.Once);
        }

        [Fact]
        public void Delete_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.Delete(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void CalculaValorBaseSimples_IdValido_DeveRetornarCTeViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cte = new CTe(18, 1) { Id = 1 };
            cte.Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1) { ValorFrete = 1000 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(cte);
            mockRepository.Setup(repo => repo.Update(cte));

            var service = new CTeService(mockRepository.Object);

            // Act
            var result = service.CalculaValorBaseSimples(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CTeViewModel>(result);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(cte), Times.Once);
        }

        [Fact]
        public void CalculaValorBaseSimples_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.CalculaValorBaseSimples(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }

        [Fact]
        public void CalculaValorBasePorDentro_IdValido_DeveRetornarCTeViewModel()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            var cte = new CTe(18, 1) { Id = 1 };
            cte.Viagem = new Viagem("Teste", "Teste", 100, DateTime.Now, 1) { ValorFrete = 1000 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(cte);
            mockRepository.Setup(repo => repo.Update(cte));

            var service = new CTeService(mockRepository.Object);

            // Act
            var result = service.CalculaValorBasePorDentro(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CTeViewModel>(result);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
            mockRepository.Verify(repo => repo.Update(cte), Times.Once);
        }

        [Fact]
        public void CalculaValorBasePorDentro_RepositoryLancaExcecao_DeveLancarExcecao()
        {
            // Arrange
            var mockRepository = new Mock<ICTeRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Throws(new Exception("Erro no repositório"));

            var service = new CTeService(mockRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => service.CalculaValorBasePorDentro(1));
            Assert.Equal("Erro no processamento:Erro no repositório!", exception.Message);
            mockRepository.Verify(repo => repo.GetById(1), Times.Once);
        }
    }
}
