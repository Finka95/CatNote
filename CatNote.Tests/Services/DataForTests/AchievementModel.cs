using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatNote.BLL.Models;

namespace CatNote.Tests.Services.DataForTests;

public class AchievementModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IEnumerable<UserModel>? Users { get; set; }
}