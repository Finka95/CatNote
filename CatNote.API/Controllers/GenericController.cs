using CatNote.BLL.Interfaces;
using CatNote.BLL.Mappers.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TModel, TDto> : ControllerBase, IGenericController<TDto>
{
    private readonly IMapper<TModel, TDto> _mapper;
    private readonly IGenericService<TModel> _service;

    public GenericController(IMapper<TModel, TDto> mapper, IGenericService<TModel> service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<TDto> Create(TDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.ToEntity(dto);

        var resultModel = await _service.Create(model, cancellationToken);

        return _mapper.FromEntity(resultModel);
        ;
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        await _service.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<TDto>> GetAll(CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetAll(cancellationToken);

        return resultModel.Select(x => _mapper.FromEntity(x)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<TDto> GetById(int id, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetById(id, cancellationToken);

        return _mapper.FromEntity(resultModel);
    }

    [HttpPut]
    public async Task<TDto> Update(TDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.ToEntity(dto);

        var resultModel = await _service.Update(model, cancellationToken);

        return _mapper.FromEntity(resultModel);
    }
}