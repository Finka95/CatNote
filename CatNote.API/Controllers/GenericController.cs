using AutoMapper;
using CatNote.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatNote.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<TModel, TDTO> : ControllerBase, IGenericController<TDTO>
{
    private readonly IMapper _mapper;
    private readonly IGenericService<TModel> _service;

    public GenericController(IMapper mapper, IGenericService<TModel> service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<TDTO> Create(TDTO dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TModel>(dto);

        var resultModel = await _service.Create(model, cancellationToken);

        return _mapper.Map<TDTO>(resultModel);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        await _service.Delete(id, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<TDTO>> GetAll(CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetAll(cancellationToken);

        return _mapper.Map<List<TDTO>>(resultModel);
    }

    [HttpGet("{id}")]
    public async Task<TDTO> GetById(int id, CancellationToken cancellationToken)
    {
        var resultModel = await _service.GetById(id, cancellationToken);

        return _mapper.Map<TDTO>(resultModel);
    }

    [HttpPut]
    public async Task<TDTO> Update(TDTO dto, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TModel>(dto);

        var resultModel = await _service.Update(model, cancellationToken);

        return _mapper.Map<TDTO>(resultModel);
    }
}
