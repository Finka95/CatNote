using CatNote.BLL.Models;
using CatNote.Domain.Enums;

namespace CatNote.BLL.Interfaces;

public interface IAchievementProcessor
{
    AchievementType AchievementType { get; }
    bool Execute(UserModel user);
}

