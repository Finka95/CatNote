﻿namespace CatNote.DAL.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string? UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
