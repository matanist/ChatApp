using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
public class GroupController: Controller
{
    private readonly IGroupService _groupService;
    private readonly IMapper _mapper;

    public GroupController(IGroupService groupService, IMapper mapper)
    {
        _groupService = groupService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
    {
        var groups = await _groupService.ListAllAsync(paginationModel);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = _mapper.Map<List<GroupModel>>(groups),
            StatusCode = 200,
            TotalCount = await _groupService.CountAsync()
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = _mapper.Map<GroupModel>(group),
            StatusCode = 200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] GroupCreateModel groupModel)
    {
        var group = _mapper.Map<Group>(groupModel);
        var groupResult = await _groupService.AddAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = _mapper.Map<GroupModel>(groupResult),
            StatusCode = 200
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] GroupUpdateModel groupModel)
    {
        var group = _mapper.Map<Group>(groupModel);
        var groupResult = await _groupService.UpdateAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            Data = _mapper.Map<GroupModel>(groupResult),
            StatusCode = 200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id)
    {
        var group = await _groupService.GetByIdAsync(id);
        await _groupService.DeleteAsync(group);
        return new ReturnModel
        {
            Success = true,
            Message = "Success",
            StatusCode = 200
        };
    }
}
