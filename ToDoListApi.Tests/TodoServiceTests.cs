using ToDoList.API;
using Xunit;

namespace TodoApi.Tests.Services;

public class TodoServiceTests
{
    [Fact]
    public void GetAll_WhenEmpty_ReturnsEmptyCollection()
    {
        var service = new ToDoService();

        var result = service.GetAllItems();

        Assert.Empty(result);
    }

    [Fact]
    public void Add_ReturnsItemWithGeneratedIdAndTrimmedTitle()
    {
        var service = new ToDoService();

        var created = service.AddItem("  Buy milk  ");

        Assert.NotEqual(Guid.Empty, created.Id);
        Assert.Equal("Buy milk", created.Title);
        Assert.False(created.IsComplete);
    }

    [Fact]
    public void Add_StoresItemSoItCanBeRetrieved()
    {
        var service = new ToDoService();

        var created = service.AddItem("Walk the dog");
        var fetched = service.GetItemById(created.Id);

        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal("Walk the dog", fetched.Title);
    }

    [Fact]
    public void GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var service = new ToDoService();

        var result = service.GetItemById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public void Delete_WhenItemExists_RemovesItAndReturnsTrue()
    {
        var service = new ToDoService();
        var created = service.AddItem("Temporary");

        var deleted = service.DeleteItem(created.Id);

        Assert.True(deleted);
        Assert.Null(service.GetItemById(created.Id));
    }

    [Fact]
    public void Delete_WhenItemDoesNotExist_ReturnsFalse()
    {
        var service = new ToDoService();

        var deleted = service.DeleteItem(Guid.NewGuid());

        Assert.False(deleted);
    }

   
}
