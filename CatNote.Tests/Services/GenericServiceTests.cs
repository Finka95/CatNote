using AutoMapper;
using CatNote.BLL.Models;
using CatNote.BLL.Services;
using CatNote.DAL.Entities;
using CatNote.DAL.Interfaces;
using CatNote.Tests.Services.DataForTests;
using Moq;
using Xunit;

namespace CatNote.Tests.Services;

public class GenericServiceTests
{
    private readonly Mock<IGenericRepository<UserEntity>> _mockGenericRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GenericService<UserModel, UserEntity> _userService;

    public GenericServiceTests()
    {
        _mockGenericRepository = new Mock<IGenericRepository<UserEntity>>();
        _mockMapper = new Mock<IMapper>();
        _userService = new GenericService<UserModel, UserEntity>(_mockMapper.Object, _mockGenericRepository.Object);
    }

    [Fact]
    public async Task Create_CreateNewUser_CallCreateMethodOfRepositoryAndReturnUserModel()
    {
        //Arrange
        var cancellationToken = new CancellationToken();

        var userModel = UserData.GetUserModel();
        var userEntity = UserData.GetUserEntity();

        _mockMapper.Setup(x => x.Map<UserEntity>(It.IsAny<UserModel>())).Returns(userEntity);
        _mockGenericRepository.Setup(x => x.Create(It.IsAny<UserEntity>(), CancellationToken.None)).ReturnsAsync(value: UserData.GetUserEntity());
        _mockMapper.Setup(x => x.Map<UserModel>(It.IsAny<UserEntity>())).Returns(userModel);
        
        //Act
        var result = await _userService.Create(UserData.GetUserModel(), cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<UserEntity>(It.IsAny<UserModel>()), Times.Once());
        Assert.Equal(userModel, result);
        _mockGenericRepository.Verify(x => x.Create(It.IsAny<UserEntity>(), cancellationToken), Times.Once);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once());
    }

    [Fact]
    public async Task Delete_DeleteUser_CallDeleteMethodOfRepository()
    {
        //Arrange
        var userId = 1;

        var cancellationToken = new CancellationToken();

        //Act
        await _userService.Delete(userId, cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.Delete(userId, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetAll_GetAllUsers_CallGetAllMethodOfRepositoryAndReturnUserModelList()
    {
        //Arrange
        var userEntityList = new List<UserEntity>();
        userEntityList.Add(UserData.GetUserEntity());

        var userModelList = new List<UserModel>();
        userModelList.Add(UserData.GetUserModel());

        var userModel = UserData.GetUserModel();

        var cancellationToken = new CancellationToken();

        _mockGenericRepository.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(userEntityList);
        _mockMapper.Setup(x => x.Map<List<UserModel>>(It.IsAny<List<UserEntity>>())).Returns(userModelList);

        //Act
        var result = await _userService.GetAll(cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.GetAll(cancellationToken), Times.Once);
        Assert.Equal(userModelList, result);
        _mockMapper.Verify(x => x.Map<List<UserModel>>(It.IsAny<List<UserEntity>>()), Times.Exactly(userModelList.Count));
    }

    [Fact]
    public async Task GetById_GetUseById_CallGetByIdOfRepositoryAndReturnUserModel()
    {
        //Arrange
        var userId = 1;

        var userEntity = UserData.GetUserEntity();

        var userModel = UserData.GetUserModel();

        var cancellationToken = new CancellationToken();

        _mockGenericRepository.Setup(x => x.GetById(userId, cancellationToken)).ReturnsAsync(userEntity);
        _mockMapper.Setup(x => x.Map<UserModel>(It.IsAny<UserEntity>())).Returns(userModel);

        //Act
        var result = await _userService.GetById(userId, cancellationToken);

        //Assert
        _mockGenericRepository.Verify(x => x.GetById(userId, cancellationToken), Times.Once);
        Assert.Equal(userModel, result);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateUser_CallUpdateOfRepositoryAndReturnUserModel()
    {
        //Arrange
        var userModel = UserData.GetUserModel();

        var userEntityResult = new UserEntity
        {
            Id = 2,
            UserName = "name2"
        };

        var userModelResult = new UserModel
        {
            Id = 2,
            UserName = "name2"
        };

        var cancellationToken = new CancellationToken();

        _mockMapper.Setup(x => x.Map<UserEntity>(It.IsAny<UserModel>())).Returns(userEntityResult);
        _mockGenericRepository.Setup(x => x.Update(It.IsAny<UserEntity>(), cancellationToken))
            .ReturnsAsync(userEntityResult);

        _mockMapper.Setup(x => x.Map<UserModel>(It.IsAny<UserEntity>())).Returns(userModelResult);

        //Act
        var result = await _userService.Update(userModel, cancellationToken);

        //Assert
        _mockMapper.Verify(x => x.Map<UserEntity>(It.IsAny<UserModel>()), Times.Once);
        _mockGenericRepository.Verify(x => x.Update(It.IsAny<UserEntity>(), cancellationToken), Times.Once);
        Assert.Equal(userModelResult, result);
        _mockMapper.Verify(x => x.Map<UserModel>(It.IsAny<UserEntity>()), Times.Once);
    }
}
