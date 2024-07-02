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

        CreateMap<Data.Message, MessageModel>().ReverseMap();
        CreateMap<Data.Message, MessageCreateModel>().ReverseMap();
        CreateMap<Data.Message, MessageUpdateModel>().ReverseMap();

        CreateMap<Data.Group, GroupModel>().ReverseMap();
        CreateMap<Data.Group, GroupCreateModel>().ReverseMap();
        CreateMap<Data.Group, GroupUpdateModel>().ReverseMap();

    }
}
