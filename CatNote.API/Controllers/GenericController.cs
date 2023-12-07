using AutoMapper;
using CatNote.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TModel, TDto> : ControllerBase, IGenericController<TDto>
{
    private readonly IMapper _mapper;
    private readonly IGenericService<TModel> _service;

    public GenericController(IMapper mapper, IGenericService<TModel> service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<TDto> Create(TDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TModel>(dto);

        var resultModel = await _service.Create(model, cancellationToken);

        return _mapper.Map<TDto>(resultModel);
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

        return _mapper.Map<List<TDto>>(resultModel);
    }

    [HttpGet("{id}")]
    public async Task<TDto> GetById(int id, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetById(id, cancellationToken);

        return _mapper.Map<TDto>(resultModel);
    }

    [HttpPut]
    public async Task<TDto> Update(TDto dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TModel>(dto);

        var resultModel = await _service.Update(model, cancellationToken);

        return _mapper.Map<TDto>(resultModel);
    }
}
