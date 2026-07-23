using System;

namespace ToDoList.API;
/// <summary>
/// Manages the collection of ToDo items
/// </summary>
public interface IToDoService
{
    List<ToDoItem> GetAllItems();
    ToDoItem? GetItemById(Guid id);
    ToDoItem AddItem(string title);
    bool DeleteItem(Guid id);

}
