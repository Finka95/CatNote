using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

public static class UserData
{
    public static UserModel UserModel =
        new() { Id = 1, UserName = "name" };

    public static UserEntity UserEntity = 
        new() { Id = 1, UserName = "name" };
}
