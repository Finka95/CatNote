using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class UserData
{
    public static UserModel GetUserModel() => new()
    {
        Id = 1,
        UserName = "name"
    };

    public static UserEntity GetUserEntity() => new()
    {
        Id = 1,
        UserName = "name"
    };
}
