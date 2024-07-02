using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
public class AuthController: Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, IConfiguration configuration, IMapper mapper)
    {
        _userService = userService;
        _configuration = configuration;
        _mapper = mapper;
    }
    [HttpPost("login")]
    public async Task<ReturnModel> Login([FromBody] LoginModel loginModel)
    {
        var user = await _userService.GetByUsernameAndPasswordAsync(loginModel.Username, loginModel.Password);
        if(user == null)
        {
            return new ReturnModel{
                Success = false,
                Message = "Invalid username or password",
                StatusCode = 400
            };
        }
        var tokenModel = new TokenModel{
            Username = user.Username,
            Role = user.Role,
            SigninKey = _configuration["Jwt:SigningKey"],
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var token = JwtToken.GenerateToken(tokenModel);
        return new ReturnModel{
            Success = true,
            Message = "Login successful",
            Data = token,
            StatusCode = 200
        };
    }
    [HttpPost("register")]
    public async Task<ReturnModel> Register([FromBody] UserCreateModel userCreateModel)
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
}
