using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ChatApp.Services;

public class MyProfiles : Profile
{
    public MyProfiles()
    {
        CreateMap<Data.User, UserModel>().ReverseMap();
        CreateMap<Data.User, UserCreateModel>().ReverseMap();
        CreateMap<Data.User, UserUpdateModel>().ReverseMap();

    }
}
