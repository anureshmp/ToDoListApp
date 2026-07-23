using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService todoService;
        private readonly ILogger<ToDoController> logger;
        public ToDoController(IToDoService toDoService, ILogger<ToDoController> logger)
        {
            this.todoService = toDoService;
            this.logger = logger;       
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            var items = this.todoService.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<IEnumerable<ToDoItem>> GetItemById(Guid id)
        {
            var item = this.todoService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return  Ok(item);
        }

        [HttpPost]
        public ActionResult<IEnumerable<ToDoItem>> Create([FromBody] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                ModelState.AddModelError(nameof(title), "Title cannot be blank");
                return ValidationProblem(ModelState);
            }

            var item = this.todoService.AddItem(title);
            logger.LogInformation("Created todo {ToDoId}", item.Id);

            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult<bool> Delete(Guid id)
        {
            var deleted = todoService.DeleteItem(id);
            if (!deleted)
            {
                return NotFound();
            }

            logger.LogInformation("Deleted todo {TodoId}", id);
            return NoContent();
        }
    }
}
