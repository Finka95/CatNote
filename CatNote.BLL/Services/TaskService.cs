using AutoMapper;
using CatNote.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.BLL.Services;

public class TaskService : GenericService<TaskModel, TaskEntity>, ITaskService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<TaskEntity> _genericRepository;

    public TaskService(IMapper mapper, IGenericRepository<TaskEntity> genericRepository)
        :base(mapper, genericRepository)
    {
        
    }

    public async Task<TaskModel> Create(TaskModel model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TaskEntity>(model);

        var resultEntity = await _genericRepository.Create(entity, cancellationToken);

        return _mapper.Map<TaskModel>(resultEntity);
    }
}