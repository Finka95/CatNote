using AutoMapper;
using CatNote.BLL.AchievementTypes;
using CatNote.BLL.Models;
using CatNote.BLL.Services;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Enums;
using CatNote.Domain.Exceptions;
using CatNote.Tests.Services.DataForTests;
using FluentAssertions;
using Moq;
using Xunit;
using TaskStatus = CatNote.Domain.Enums.TaskStatus;

namespace CatNote.Tests.Services;

public class AchievementServiceTests
{
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IAchievementRepository> _mockAchievementRepository;
    private readonly AchievementService _achievementService;
    private readonly Mock<IUserRepository> _mockUserRepository;

    public AchievementServiceTests()
    {
        _mockAchievementRepository = new Mock<IAchievementRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockUserRepository = new Mock<IUserRepository>();
        _achievementService = new AchievementService(_mockMapper.Object, _mockAchievementRepository.Object, _mockUserRepository.Object);
    }

    [Fact]
    public async Task Create_CorrectModelPass_AchievementModel()
    {
        //Arrange
        var cancellationToken = new CancellationToken();

        var achievementModel = AchievementData.AchievementModel;
        var achievementEntity = AchievementData.AchievementEntity;

        SetupMapper(achievementEntity, achievementModel);
        _mockAchievementRepository.Setup(x => x.Create(It.IsAny<AchievementEntity>(), CancellationToken.None)).ReturnsAsync(value: AchievementData.AchievementEntity);
        SetupMapper(achievementModel, achievementEntity);

        //Act
        var result = await _achievementService.Create(AchievementData.AchievementModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<AchievementEntity>(It.IsAny<Achievement>()), Times.Once());

        result.Should().BeOfType<Achievement>();
        result.Id.Should().Be(achievementEntity.Id);
        result.Title.Should().Be(achievementEntity.Title);
        result.Description.Should().Be(achievementEntity.Description);

        _mockAchievementRepository.Verify(x => x.Create(It.IsAny<AchievementEntity>(), cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<Achievement>(It.IsAny<AchievementEntity>()), Times.Once());
    }

    [Fact]
    public async Task Delete_AchievementByCorrectId_Success()
    {
        //Arrange
        var achievementId = 1;

        var cancellationToken = new CancellationToken();

        //Act
        await _achievementService.Delete(achievementId, cancellationToken);

        //Assert
        _mockAchievementRepository.Verify(x => x.Delete(achievementId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAll_GetAllAchievements_AchievementModelList()
    {
        //Arrange
        var achievementEntityList = new List<AchievementEntity> { AchievementData.AchievementEntity };

        var achievementModelList = new List<Achievement>();
        var achievementModel = AchievementData.AchievementModel;
        achievementModelList.Add(achievementModel);

        var cancellationToken = new CancellationToken();

        _mockAchievementRepository.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(achievementEntityList);
        SetupMapper(achievementModelList, achievementEntityList);

        //Act
        var result = await _achievementService.GetAll(cancellationToken);

        //Assert
        _mockAchievementRepository.Verify(x => x.GetAll(cancellationToken), Times.Once);

        result.Should().BeOfType<List<Achievement>>()
            .And.Contain(achievementModel);

        _mockMapper.Verify(x => x.Map<List<Achievement>>(It.IsAny<List<AchievementEntity>>()), Times.Exactly(achievementModelList.Count));
    }

    [Fact]
    public async Task GetById_GetAchievementByCorrectIdPass_AchievementModel()
    {
        //Arrange
        var achievementId = 1;

        var achievementEntity = AchievementData.AchievementEntity;

        var achievementModel = AchievementData.AchievementModel;

        var cancellationToken = new CancellationToken();

        _mockAchievementRepository.Setup(x => x.GetById(achievementId, cancellationToken)).ReturnsAsync(achievementEntity);
        SetupMapper(achievementModel, achievementEntity);

        //Act
        var result = await _achievementService.GetById(achievementId, cancellationToken);

        //Assert
        _mockAchievementRepository.Verify(x => x.GetById(achievementId, cancellationToken), Times.Once);

        result.Should().BeOfType<Achievement>();
        result.Id.Should().Be(achievementModel.Id);
        result.Title.Should().Be(achievementModel.Title);
        result.Description.Should().Be(achievementModel.Description);

        _mockMapper.Verify(x => x.Map<Achievement>(It.IsAny<AchievementEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetById_GetAchievementByIncorrectId_Null()
    {
        //Arrange
        var achievementId = 5;

        var cancellationToken = new CancellationToken();

        _mockAchievementRepository.Setup(x => x.GetById(achievementId, cancellationToken)).ReturnsAsync((AchievementEntity?)null!);

        //Act
        var result = await _achievementService.GetById(achievementId, cancellationToken);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Get_CheckAchievementToAddByCorrectIdPass_Success()
    {
        var cancellationToken = new CancellationToken();

        var user = new UserModel
        {
            Id = 1,
            UserName = "Default",
            Tasks = new List<TaskModel>
            {
                new ()
                {
                    Id = 1,
                    Title = "Default",
                    Status = TaskStatus.ToDo,
                    Date = DateTime.Today
                }
            },
            Achievements = new List<Achievement>()
        };

        var userEntity = new UserEntity
        {
            Id = 1,
            UserName = "Default",
            Tasks = new List<TaskEntity>
            {
                new()
                {
                    Id = 1,
                    Title = "Default",
                    Status = TaskStatus.ToDo,
                    Date = DateTime.Today
                }
            },
            Achievements = new List<AchievementEntity>()
        };

        SetupMapper(user, userEntity);

        _mockUserRepository.Setup(x => x.GetUserByIdWithTasksAchievements(1, cancellationToken))
            .ReturnsAsync(userEntity);

        var achievement = AchievementData.AchievementModel;
        var achievementEntity = AchievementData.AchievementEntity;

        SetupMapper(achievement, achievementEntity);

         _mockAchievementRepository.Setup(x => x
            .GetAchievementByTaskCountAchievementType(1, AchievementType.Add, cancellationToken))
            .ReturnsAsync(achievementEntity);

        await _achievementService.CheckAchievementToAdd(1, cancellationToken);

        _mockAchievementRepository.Verify(x => x.AddConnectionBetweenUserAndAchievement(1, 1, cancellationToken), Times.Once);
        _mockAchievementRepository.Verify(x => x.GetAchievementByTaskCountAchievementType(1, AchievementType.Add, cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<Achievement>(It.IsAny<AchievementEntity>()), Times.Once);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task Get_CheckAchievementToCompleteByCorrectIdPass_Success()
    {
        var cancellationToken = new CancellationToken();

        var user = new UserModel
        {
            Id = 1,
            UserName = "Default",
            Tasks = new List<TaskModel>
            {
                new ()
                {
                    Id = 1,
                    Title = "Default",
                    Status = TaskStatus.Done,
                    Date = DateTime.Today
                }
            },
            Achievements = new List<Achievement>()
        };

        var userEntity = new UserEntity
        {
            Id = 1,
            UserName = "Default",
            Tasks = new List<TaskEntity>
            {
                new()
                {
                    Id = 1,
                    Title = "Default",
                    Status = TaskStatus.Done,
                    Date = DateTime.Today
                }
            },
            Achievements = new List<AchievementEntity>()
        };

        SetupMapper(user, userEntity);

        _mockUserRepository.Setup(x => x.GetUserByIdWithTasksAchievements(1, cancellationToken))
            .ReturnsAsync(userEntity);

        var achievement = new Achievement
        {
            Id = 1,
            Title = "Default",
            Description = "Default",
            TaskCount = 1,
            Type = AchievementType.Completed
        };

        var achievementEntity = new AchievementEntity
        {
            Id = 1,
            Title = "Default",
            Description = "Default",
            TaskCount = 1,
            Type = AchievementType.Completed
        };

        SetupMapper(achievement, achievementEntity);

         _mockAchievementRepository.Setup(x => x
            .GetAchievementByTaskCountAchievementType(1, AchievementType.Completed, cancellationToken))
            .ReturnsAsync(achievementEntity);

        await _achievementService.CheckAchievementToComplete(1, cancellationToken);

        _mockAchievementRepository.Verify(x => x.AddConnectionBetweenUserAndAchievement(1, 1, cancellationToken), Times.Once);
        _mockAchievementRepository.Verify(x => x.GetAchievementByTaskCountAchievementType(1, AchievementType.Completed, cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<Achievement>(It.IsAny<AchievementEntity>()), Times.Once);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateAchievementByCorrectIdPass_AchievementModel()
    {
        //Arrange
        var achievementModel = AchievementData.AchievementModel;
        var achievementEntity = AchievementData.AchievementEntity;

        var achievementEntityResult = new AchievementEntity { Id = 1, Title = "default2", Description = "default2" };

        var achievementModelResult = new Achievement { Id = 1, Title = "default2", Description = "default2" };

        var cancellationToken = new CancellationToken();

        SetupMapper(achievementModel, achievementEntity);
        SetupMapper(achievementEntity, achievementModel);
        SetupMapper(achievementModelResult, achievementEntityResult);

        _mockAchievementRepository.Setup(x => x.GetById(achievementModel.Id, cancellationToken))
            .ReturnsAsync(achievementEntity);

        _mockAchievementRepository.Setup(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken))
            .ReturnsAsync(achievementEntityResult);

        //Act
        var result = await _achievementService.Update(achievementModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<AchievementEntity>(It.IsAny<Achievement>()), Times.Once);
        _mockAchievementRepository.Verify(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken), Times.Once);

        result.Should().BeOfType<Achievement>();
        result.Id.Should().Be(achievementModelResult.Id);
        result.Title.Should().Be(achievementModelResult.Title);
        result.Description.Should().Be(achievementModelResult.Description);

        _mockMapper.Verify(x => x.Map<Achievement>(It.IsAny<AchievementEntity>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Update_UpdateAchievementByIncorrectIdPass_NotFoundException()
    {
        //Arrange
        var achievementModel = AchievementData.AchievementModel;
        var achievementEntity = AchievementData.AchievementEntity;

        var cancellationToken = new CancellationToken();

        SetupMapper(achievementEntity, achievementModel);

        _mockAchievementRepository.Setup(x => x.GetById(achievementModel.Id, cancellationToken))
            .ReturnsAsync((AchievementEntity?)null);

        _mockAchievementRepository.Setup(x => x.Update(It.IsAny<AchievementEntity>(), cancellationToken))
            .ReturnsAsync((AchievementEntity?)null!);

        ////Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _achievementService.Update(achievementModel, cancellationToken));
    }

    private void SetupMapper<T1, T2>(T1 returnElement, T2 startElement)
    {
        _mockMapper.Setup(x => x.Map<T1>(startElement)).Returns(returnElement);
    }
}
