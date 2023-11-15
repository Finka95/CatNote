using AutoMapper;
using CatNote.BLL.Models;
using CatNote.BLL.Services;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Tests.Services.DataForTests;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatNote.Tests.Services;

public class AchievementGenericServiceTests
{
    private readonly Mock<IGenericRepository<AchievementEntity>> _mockGenericRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GenericService<AchievementModel, AchievementEntity> _achievementService;

    public AchievementGenericServiceTests()
    {
        _mockGenericRepository = new Mock<IGenericRepository<AchievementEntity>>();
        _mockMapper = new Mock<IMapper>();
        _achievementService = new GenericService<AchievementModel, AchievementEntity>(_mockMapper.Object, _mockGenericRepository.Object);
    }

    [Fact]
    public async Task Create_CorrectModelPass_ReturnAchievementModel()
    {
        //Arrange
        var cancellationToken = new CancellationToken();

        var achievementModel = AchievementData.AchievementModel;
        var achievementEntity = AchievementData.AchievementEntity;

        SetupMapper<AchievementEntity, AchievementModel>(achievementEntity);
        _mockGenericRepository.Setup(x => x.Create(It.IsAny<AchievementEntity>(), CancellationToken.None)).ReturnsAsync(value: AchievementData.AchievementEntity);
        SetupMapper<AchievementModel, AchievementEntity>(achievementModel);

        //Act
        var result = await _achievementService.Create(AchievementData.AchievementModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<AchievementEntity>(It.IsAny<AchievementModel>()), Times.Once());

        result.Should().BeOfType<AchievementModel>();
        result.Id.Should().Be(achievementEntity.Id);
        result.Title.Should().Be(achievementEntity.Title);
        result.Description.Should().Be(achievementEntity.Description);

        _mockGenericRepository.Verify(x => x.Create(It.IsAny<AchievementEntity>(), cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<AchievementModel>(It.IsAny<AchievementEntity>()), Times.Once());
    }

    [Fact]
    public async Task Delete_AchievementByCorrectId_ReturnSuccess()
    {
        //Arrange
        var achievementId = 1;

        var cancellationToken = new CancellationToken();

        //Act
        await _achievementService.Delete(achievementId, cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.Delete(achievementId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAll_GetAllAchievements_ReturnAchievementModelList()
    {
        //Arrange
        var achievementEntityList = new List<AchievementEntity>();
        achievementEntityList.Add(AchievementData.AchievementEntity);

        var achievementModelList = new List<AchievementModel>();
        var achievementModel = AchievementData.AchievementModel;
        achievementModelList.Add(achievementModel);

        var cancellationToken = new CancellationToken();

        _mockGenericRepository.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(achievementEntityList);
        SetupMapper<List<AchievementModel>, List<AchievementEntity>>(achievementModelList);

        //Act
        var result = await _achievementService.GetAll(cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.GetAll(cancellationToken), Times.Once);

        result.Should().BeOfType<List<AchievementModel>>()
            .And.Contain(achievementModel);

        _mockMapper.Verify(x => x.Map<List<AchievementModel>>(It.IsAny<List<AchievementEntity>>()), Times.Exactly(achievementModelList.Count));
    }

    [Fact]
    public async Task GetById_GetAchievementByCorrectIdPass_ReturnAchievementModel()
    {
        //Arrange
        var achievementId = 1;

        var achievementEntity = AchievementData.AchievementEntity;

        var achievementModel = AchievementData.AchievementModel;

        var cancellationToken = new CancellationToken();

        _mockGenericRepository.Setup(x => x.GetById(achievementId, cancellationToken)).ReturnsAsync(achievementEntity);
        SetupMapper<AchievementModel, AchievementEntity>(achievementModel);

        //Act
        var result = await _achievementService.GetById(achievementId, cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.GetById(achievementId, cancellationToken), Times.Once);

        result.Should().BeOfType<AchievementModel>();
        result.Id.Should().Be(achievementModel.Id);
        result.Title.Should().Be(achievementModel.Title);
        result.Description.Should().Be(achievementModel.Description);

        _mockMapper.Verify(x => x.Map<AchievementModel>(It.IsAny<AchievementEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetById_GetAchievementByIncorrectId_ReturnNull()
    {
        //Arrange
        var achievementId = 5;

        var cancellationToken = new CancellationToken();

        _mockGenericRepository.Setup(x => x.GetById(achievementId, cancellationToken)).ReturnsAsync((AchievementEntity?)null);

        //Act
        var result = await _achievementService.GetById(achievementId, cancellationToken);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Update_UpdateAchievementByCorrectIdPass_ReturnAchievementModel()
    {
        //Arrange
        var achievementModel = AchievementData.AchievementModel;

        var achievementEntityResult = new AchievementEntity
        {
            Id = 2,
            Title = "default2",
            Description = "default2"
        };

        var achievementModelResult = new AchievementModel
        {
            Id = 2,
            Title = "default2",
            Description = "default2"
        };

        var cancellationToken = new CancellationToken();

        SetupMapper<AchievementEntity, AchievementModel>(achievementEntityResult);
        _mockGenericRepository.Setup(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken))
            .ReturnsAsync(achievementEntityResult);

        SetupMapper<AchievementModel, AchievementEntity>(achievementModelResult);

        //Act
        var result = await _achievementService.Update(achievementModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<AchievementEntity>(It.IsAny<AchievementModel>()), Times.Once);
        _mockGenericRepository.Verify(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken), Times.Once);

        result.Should().BeOfType<AchievementModel>();
        result.Id.Should().Be(achievementModelResult.Id);
        result.Title.Should().Be(achievementModelResult.Title);
        result.Description.Should().Be(achievementModelResult.Description);

        _mockMapper.Verify(x => x.Map<AchievementModel>(It.IsAny<AchievementEntity>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateAchievementByIncorrectIdPass_ReturnNull()
    {
        //Arrange
        var achievementModel = AchievementData.AchievementModel;

        var achievementEntityResult = new AchievementEntity
        {
            Id = 2,
            Title = "default2",
            Description = "default2"
        };

        var cancellationToken = new CancellationToken();

        SetupMapper<AchievementEntity, AchievementModel>(achievementEntityResult);
        _mockGenericRepository.Setup(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken))
            .ReturnsAsync((AchievementEntity)null);

        //Act
        var result = await _achievementService.Update(achievementModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<AchievementEntity>(It.IsAny<AchievementModel>()), Times.Once);
        _mockGenericRepository.Verify(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken), Times.Once);

        result.Should().Be(null);

        _mockMapper.Verify(x => x.Map<AchievementModel>(It.IsAny<AchievementEntity>()), Times.Once);
    }

    private void SetupMapper<T1, T2>(T1 element)
    {
        _mockMapper.Setup(x => x.Map<T1>(It.IsAny<T2>())).Returns(element);
    }
}
