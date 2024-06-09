using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery]PaginationModel paginationModel)
    {
        var users = await _userService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success = true,
            Message = "Success",
            Data = _mapper.Map<List<UserModel>>(users),
            StatusCode = 200,
            TotalCount = await _userService.CountAsync()
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        return new ReturnModel{
            Success = true,
            Message = "Success",
            Data = user,
            StatusCode = 200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] UserCreateModel userCreateModel)
    {
        var newUserr = _mapper.Map<User>(userCreateModel);
        var newUser = await _userService.AddAsync(newUserr);
        return new ReturnModel{
            Success = true,
            Message = "User created successfully",
            Data = newUser,
            StatusCode = 201
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] UserUpdateModel userModel)
    {
        var user = _mapper.Map<User>(userModel);
        var updatedUser = await _userService.UpdateAsync(user);
        return new ReturnModel{
            Success = true,
            Message = "User updated successfully",
            Data = _mapper.Map<UserModel>(updatedUser),
            StatusCode = 200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        await _userService.DeleteAsync(user);
        return new ReturnModel{
            Success = true,
            Message = "User deleted successfully",
            StatusCode = 200
        };
    }
}
