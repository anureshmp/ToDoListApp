using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ToDoList.API;
using ToDoListApi;
namespace ToDoListApi.Tests;

public class ToDoControllerTest
{
    private readonly Mock<IToDoService> todoServiceMock = new();
    private readonly ToDoController controller;

    public ToDoControllerTest()
    {
        controller = new ToDoController(todoServiceMock.Object, NullLogger<ToDoController>.Instance);
    }

    private static ToDoItem MakeItem(string title)
    {
        return new ToDoItem ()
        {
            Id = Guid.NewGuid(),
            Title = title,
            IsComplete = false,
            CreatedAt = DateTime.Now
        };
    }

    [Fact]
    public void GetAllItems_ReturnsAllItems()
    {
        var items = new List<ToDoItem> { MakeItem("First Todo"), MakeItem("Second Todo") };
        todoServiceMock.Setup(s => s.GetAllItems()).Returns(items);

        var result = controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(okResult.Value);
        Assert.Equal(items.Select(i => i.Id), response.Select(i => i.Id));

    }

    [Fact]
    public void GetById_WhenFound_ReturnsOk()
    {
        var item = MakeItem("First Todo");
        todoServiceMock.Setup(s => s.GetItemById(item.Id)).Returns(item);

        var result = controller.GetItemById(item.Id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<ToDoItem>(okResult.Value);

        Assert.Equal(item.Id, response.Id);
    }

    [Fact]
    public void GetById_WhenNotFound_ReturnsNotFound()
    {
        todoServiceMock.Setup(s => s.GetItemById(It.IsAny<Guid>())).Returns((ToDoItem?)null);

        var result = controller.GetItemById(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]
    public void Delete_WhenItemExists_ReturnsNoContent()
    {
        var id = Guid.NewGuid();

        todoServiceMock.Setup(s => s.DeleteItem(id)).Returns(true);
        var result = controller.Delete(id);

        Assert.IsType<NoContentResult>(result.Result);
    }

       [Fact]
    public void Delete_WhenItemDoesNotExist_ReturnsNotFound()
    {
        var id = Guid.NewGuid();
        todoServiceMock.Setup(s => s.DeleteItem(id)).Returns(false);

        var result = controller.Delete(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }

 
}
