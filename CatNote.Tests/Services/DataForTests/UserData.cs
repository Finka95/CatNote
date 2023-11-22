using CatNote.BLL.Models;
using CatNote.DAL.Entities;

namespace CatNote.Tests.Services.DataForTests;

internal static class UserData
{
    internal static UserModel UserModel =
        new() { Id = 1, UserName = "name" };

    internal static UserEntity UserEntity = 
        new() { Id = 1, UserName = "name" };
}
