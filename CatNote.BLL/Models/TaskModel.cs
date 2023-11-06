﻿using CatNote.DAL.Entities;

namespace CatNote.BLL.Models;

public class TaskModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}
