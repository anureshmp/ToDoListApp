using System;

namespace ToDoList.API;
/// <summary>
/// A ToDo item held in in-memory store 
/// </summary>
public class ToDoItem
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required bool IsComplete { get; set; }
    public required DateTime CreatedAt { get; set; }
}
