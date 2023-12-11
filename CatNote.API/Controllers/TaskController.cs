﻿using AutoMapper;
using CatNote.API.DTO;
using CatNote.BLL.Interfaces;
using CatNote.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : GenericController<TaskModel, TaskDTO>
{
    public TaskController(IMapper mapper, IGenericService<TaskModel> service)
        : base(mapper, service)
    {
    }
}
