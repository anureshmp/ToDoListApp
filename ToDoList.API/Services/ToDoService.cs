using System;
using System.Collections.Concurrent;

namespace ToDoList.API;

public class ToDoService : IToDoService
{
    private readonly ConcurrentDictionary<Guid, ToDoItem> items = [];
    public ToDoItem AddItem(string title)
    {
        var item = new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            IsComplete = false,
            CreatedAt = DateTime.Now
        };

        items[item.Id] = item;
        return item;

    }

    public bool DeleteItem(Guid id)
    {
        if (!items.ContainsKey(id))
        {
            return false;
        }

        items.TryRemove(id, out _);
        return true;
    }

    public List<ToDoItem> GetAllItems()
    {
        return items.Values.OrderBy(i => i.CreatedAt).ToList();
    }

    public ToDoItem? GetItemById(Guid id)
    {
        return items.TryGetValue(id, out var item) ? item : null;
    }
}
