using AutoMapper;
using CatNote.BLL.Models;
using CatNote.BLL.Services;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Domain.Exceptions;
using CatNote.Tests.Services.DataForTests;
using FluentAssertions;
using Moq;
using Xunit;

namespace CatNote.Tests.Services;

public class UserGenericServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserService _userService;

    public UserGenericServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _userService = new UserService(_mockMapper.Object, _mockUserRepository.Object);
    }

    [Fact]
    public async Task Create_CorrectModelPass_UserModel()
    {
        //Arrange
        var cancellationToken = new CancellationToken();

        var userModel = UserData.UserModel;
        var userEntity = UserData.UserEntity;

        SetupMapper(userEntity, userModel);
        _mockUserRepository.Setup(x => x.Create(It.IsAny<UserEntity>(), CancellationToken.None)).ReturnsAsync(value: UserData.UserEntity);
        SetupMapper(userModel, userEntity);
        
        //Act
        var result = await _userService.Create(UserData.UserModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<UserEntity>(It.IsAny<UserModel>()), Times.Once());

        result.Should().BeOfType<UserModel>();
        result.Id.Should().Be(userModel.Id);
        result.UserName.Should().Be(userModel.UserName);

        _mockUserRepository.Verify(x => x.Create(It.IsAny<UserEntity>(), cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once());
    }

    [Fact]
    public async Task Delete_UserById_Success()
    {
        //Arrange
        var userId = 1;

        var cancellationToken = new CancellationToken();

        //Act
        await _userService.Delete(userId, cancellationToken);

        //Assert
        _mockUserRepository.Verify(x => x.Delete(userId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAll_GetAllUsers_UserModelList()
    {
        //Arrange
        var userEntityList = new List<UserEntity> { UserData.UserEntity };

        var userModelList = new List<UserModel>();
        var userModel = UserData.UserModel;
        userModelList.Add(userModel);

        var cancellationToken = new CancellationToken();

        _mockUserRepository.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(userEntityList);
        SetupMapper(userModelList, userEntityList);

        //Act
        var result = await _userService.GetAll(cancellationToken);

        //Assert
        _mockUserRepository.Verify(x => x.GetAll(cancellationToken), Times.Once);

        result.Should().BeOfType<List<UserModel>>()
            .And.Contain(userModel);

        _mockMapper.Verify(x => x.Map<List<UserModel>>(It.IsAny<List<UserEntity>>()), Times.Exactly(userModelList.Count));
    }

    [Fact]
    public async Task GetById_GetUserByCorrectIdPass_UserModel()
    {
        //Arrange
        var userId = 1;

        var userEntity = UserData.UserEntity;

        var userModel = UserData.UserModel;

        var cancellationToken = new CancellationToken();

        _mockUserRepository.Setup(x => x.GetById(userId, cancellationToken)).ReturnsAsync(userEntity);
        SetupMapper(userModel, userEntity);

        //Act
        var result = await _userService.GetById(userId, cancellationToken);

        //Assert
        _mockUserRepository.Verify(x => x.GetById(userId, cancellationToken), Times.Once);

        result.Should().BeOfType<UserModel>();
        result.Id.Should().Be(userModel.Id);
        result.UserName.Should().Be(userModel.UserName);

        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetById_GetUserByIncorrectId_Null()
    {
        //Arrange
        var userId = 5;

        var cancellationToken = new CancellationToken();

        _mockUserRepository.Setup(x => x.GetById(userId, cancellationToken)).ReturnsAsync((UserEntity?)null!);

        //Act
        var result = await _userService.GetById(userId, cancellationToken);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByUserName_GetUserByCorrectIdPass_UserModel()
    {
        //Arrange
        var userName = "name";

        var userModel = UserData.UserModel;
        var userEntity = UserData.UserEntity;
        var cancellationToken = new CancellationToken();

        _mockUserRepository.Setup(x => x.GetUserByUserName(userName, cancellationToken)).ReturnsAsync(userEntity);
        SetupMapper(userModel, userEntity);

        //Act
        var result = await _userService.GetUserByUserName(userName, cancellationToken);

        //Assert
        result.Should().BeOfType<UserModel>();
        result!.UserName.Should().Be(userName);
    }

    [Fact]
    public async Task GetByUserName_GetUserByIncorrectUserNamePass_Null()
    {
        //Arrange
        var userName = "name";

        var cancellationToken = new CancellationToken();
        _mockUserRepository.Setup(x => x.GetUserByUserName(userName, cancellationToken)).ReturnsAsync((UserEntity?)null);

        //Act
        var result = await _userService.GetUserByUserName(userName, cancellationToken);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Update_UserModelWithCorrectId_UpdatedUserModel()
    {
        //Arrange
        var userModel = UserData.UserModel;
        var userEntity = UserData.UserEntity;
        var userEntityResult = new UserEntity { Id = 1, UserName = "name2" };
        var userModelResult = new UserModel { Id = 1, UserName = "name2" };
        var cancellationToken = new CancellationToken();

        SetupMapper(userModel, userEntity);
        SetupMapper(userEntity, userModel);
        SetupMapper(userModelResult, userEntityResult);

        _mockUserRepository.Setup(x => x.GetById(userModel.Id, cancellationToken))
            .ReturnsAsync(userEntity);

        _mockUserRepository.Setup(x => x.Update(userEntity, cancellationToken))
            .ReturnsAsync(userEntityResult);

        //Act
        var result = await _userService.Update(userModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<UserEntity>(It.IsAny<UserModel>()), Times.Once);
        _mockUserRepository.Verify(x => x.Update(It.IsAny<UserEntity>(), cancellationToken), Times.Once);
        result.Should().BeOfType<UserModel>();
        result.Id.Should().Be(userModelResult.Id);
        result.UserName.Should().Be(userModelResult.UserName);

        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Update_UpdateUserByIncorrectIdPass_NotFoundException()
    {
        //Arrange
        var userModel = UserData.UserModel;
        var userEntity = UserData.UserEntity;

        var cancellationToken = new CancellationToken();

        SetupMapper(userEntity, userModel);

        _mockUserRepository.Setup(x => x.GetById(userModel.Id, cancellationToken))
            .ReturnsAsync((UserEntity?)null);

        _mockUserRepository.Setup(x => x.Update(It.IsAny<UserEntity>(), cancellationToken))
            .ReturnsAsync((UserEntity?)null!);

        //Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _userService.Update(userModel, cancellationToken));
    }

    private void SetupMapper<T1, T2>(T1 returnElement, T2 startElement)
    {
        _mockMapper.Setup(x => x.Map<T1>(startElement)).Returns(returnElement);
    }
}
