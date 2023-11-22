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

//public class TaskGenericServiceTests
//{
//    private readonly Mock<IGenericRepository<TaskEntity>> _mockGenericRepository;
//    private readonly Mock<IMapper> _mockMapper;
//    private readonly GenericService<TaskModel, TaskEntity> _taskService;

//    public TaskGenericServiceTests()
//    {
//        _mockGenericRepository = new Mock<IGenericRepository<TaskEntity>>();
//        _mockMapper = new Mock<IMapper>();
//        _taskService = new GenericService<TaskModel, TaskEntity>(_mockMapper.Object, _mockGenericRepository.Object);
//    }

//    [Fact]
//    public async Task Create_CorrectModelPass_ReturnTaskModel()
//    {
//        //Arrange
//        var cancellationToken = new CancellationToken();

//        var taskModel = TaskData.GetTaskModel();
//        var taskEntity = TaskData.GetTaskEntity();

//        SetupMapper<TaskEntity, TaskModel>(taskEntity);
//        _mockGenericRepository.Setup(x => x.Create(It.IsAny<TaskEntity>(), CancellationToken.None)).ReturnsAsync(value: TaskData.GetTaskEntity());
//        SetupMapper<TaskModel, TaskEntity>(taskModel);

//        //Act
//        var result = await _taskService.Create(TaskData.GetTaskModel(), cancellationToken);

//        //Assert
//        _mockMapper.Verify(x => x.Map<TaskEntity>(It.IsAny<TaskModel>()), Times.Once());

//        result.Should().BeOfType<TaskModel>();
//        result.Id.Should().Be(taskEntity.Id);
//        result.Title.Should().Be(taskEntity.Title);
//        result.Date.Should().Be(taskEntity.Date);
//        result.Status.Should().Be(taskEntity.Status);

//        _mockGenericRepository.Verify(x => x.Create(It.IsAny<TaskEntity>(), cancellationToken), Times.Once);
//        _mockMapper.Verify(x => x.Map<TaskModel>(It.IsAny<TaskEntity>()), Times.Once());
//    }

//    [Fact]
//    public async Task Delete_TaskByCorrectId_ReturnSuccess()
//    {
//        //Arrange
//        var taskId = 1;

//        var cancellationToken = new CancellationToken();

//        //Act
//        await _taskService.Delete(taskId, cancellationToken);

//        //Assert
//        _mockGenericRepository.Verify(x => x.Delete(taskId, cancellationToken), Times.Once);
//    }

//    [Fact]
//    public async Task GetAll_GetAllTasks_ReturnTaskModelList()
//    {
//        //Arrange
//        var taskEntityList = new List<TaskEntity>();
//        taskEntityList.Add(TaskData.GetTaskEntity());

//        var taskModelList = new List<TaskModel>();
//        var taskModel = TaskData.GetTaskModel();
//        taskModelList.Add(taskModel);

//        var cancellationToken = new CancellationToken();

//        _mockGenericRepository.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(taskEntityList);
//        SetupMapper<List<TaskModel>, List<TaskEntity>>(taskModelList);

//        //Act
//        var result = await _taskService.GetAll(cancellationToken);

//        //Assert
//        _mockGenericRepository.Verify(x => x.GetAll(cancellationToken), Times.Once);

//        result.Should().BeOfType<List<TaskModel>>()
//            .And.Contain(taskModel);

//        _mockMapper.Verify(x => x.Map<List<TaskModel>>(It.IsAny<List<TaskEntity>>()), Times.Exactly(taskModelList.Count));
//    }

//    [Fact]
//    public async Task GetById_GetTaskByCorrectIdPass_ReturnTaskModel()
//    {
//        //Arrange
//        var taskId = 1;

//        var taskEntity = TaskData.GetTaskEntity();

//        var taskModel = TaskData.GetTaskModel();

//        var cancellationToken = new CancellationToken();

//        _mockGenericRepository.Setup(x => x.GetById(taskId, cancellationToken)).ReturnsAsync(taskEntity);
//        SetupMapper<TaskModel, TaskEntity>(taskModel);

//        //Act
//        var result = await _taskService.GetById(taskId, cancellationToken);

//        //Assert
//        _mockGenericRepository.Verify(x => x.GetById(taskId, cancellationToken), Times.Once);

//        result.Should().BeOfType<TaskModel>();
//        result.Id.Should().Be(taskModel.Id);
//        result.Title.Should().Be(taskModel.Title);
//        result.Date.Should().Be(taskModel.Date);
//        result.Status.Should().Be(taskModel.Status);

//        _mockMapper.Verify(x => x.Map<TaskModel>(It.IsAny<TaskEntity>()), Times.Once);
//    }

//    [Fact]
//    public async Task GetById_GetTaskByIncorrectId_ReturnNull()
//    {
//        //Arrange
//        var taskId = 5;

//        var cancellationToken = new CancellationToken();

//        _mockGenericRepository.Setup(x => x.GetById(taskId, cancellationToken)).ReturnsAsync((TaskEntity)null);

//        //Act
//        var result = await _taskService.GetById(taskId, cancellationToken);

//        //Assert
//        result.Should().BeNull();
//    }

//    [Fact]
//    public async Task Update_UpdateTaskByCorrectIdPass_ReturnTaskModel()
//    {
//        //Arrange
//        var taskModel = TaskData.GetTaskModel();

//        var taskEntityResult = new TaskEntity
//        {
//            Id = 2,
//            Title = "default2",
//            Date = DateTime.UtcNow,
//            Status = Domain.Enums.TaskStatus.Done
//        };

//        var taskModelResult = new TaskModel
//        {
//            Id = 2,
//            Title = "default2",
//            Date = DateTime.UtcNow,
//            Status = Domain.Enums.TaskStatus.Done
//        };

//        var cancellationToken = new CancellationToken();

//        SetupMapper<TaskEntity, TaskModel>(taskEntityResult);
//        _mockGenericRepository.Setup(x => x.Update(It.IsAny<TaskEntity>(), cancellationToken))
//            .ReturnsAsync(taskEntityResult);

//        SetupMapper<TaskModel, TaskEntity>(taskModelResult);

//        //Act
//        var result = await _taskService.Update(taskModel, cancellationToken);

//        //Assert
//        _mockMapper.Verify(x => x.Map<TaskEntity>(It.IsAny<TaskModel>()), Times.Once);
//        _mockGenericRepository.Verify(x => x.Update(It.IsAny<TaskEntity>(), cancellationToken), Times.Once);

//        result.Should().BeOfType<TaskModel>();
//        result.Id.Should().Be(taskModelResult.Id);
//        result.Title.Should().Be(taskModelResult.Title);
//        result.Date.Should().Be(taskModelResult.Date);
//        result.Status.Should().Be(taskModelResult.Status);

//        _mockMapper.Verify(x => x.Map<TaskModel>(It.IsAny<TaskEntity>()), Times.Once);
//    }

//    [Fact]
//    public async Task Update_UpdateTaskByIncorrectIdPass_ReturnNull()
//    {
//        //Arrange
//        var taskModel = TaskData.GetTaskModel();

//        var taskEntityResult = new TaskEntity
//        {
//            Id = 2,
//            Title = "default2",
//            Date = DateTime.UtcNow,
//            Status = Domain.Enums.TaskStatus.Done
//        };

//        var cancellationToken = new CancellationToken();

//        SetupMapper<TaskEntity, TaskModel>(taskEntityResult);
//        _mockGenericRepository.Setup(x => x.Update(It.IsAny<TaskEntity>(), cancellationToken))
//            .ReturnsAsync((TaskEntity)null);

//        //Act
//        var result = await _taskService.Update(taskModel, cancellationToken);

//        //Assert
//        _mockMapper.Verify(x => x.Map<TaskEntity>(It.IsAny<TaskModel>()), Times.Once);
//        _mockGenericRepository.Verify(x => x.Update(It.IsAny<TaskEntity>(), cancellationToken), Times.Once);

//        result.Should().Be(null);

//        _mockMapper.Verify(x => x.Map<TaskModel>(It.IsAny<TaskEntity>()), Times.Once);
//    }

//    private void SetupMapper<T1, T2>(T1 element)
//    {
//        _mockMapper.Setup(x => x.Map<T1>(It.IsAny<T2>())).Returns(element);
//    }
//}
