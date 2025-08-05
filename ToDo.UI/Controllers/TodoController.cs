using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.UI.Const;
using ToDo.UI.DTOs.TodoDto;
using ToDo.UI.Service;

namespace ToDo.UI.Controllers
{
    public class TodoController : Controller
    {
        private readonly IToDoService _context;
        public TodoController(IToDoService toDoService)
        {
            _context = toDoService ??
                throw new ArgumentNullException(nameof(toDoService));
        }
        // GET: TodoController
        public async Task<ActionResult> Index()
        {
            var todos = await _context.GetAllTodosAsync();
            return View(todos);
        }

        // GET: TodoController/Details/5
        public async Task< ActionResult> Details(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            return View(todo);
        }

        // GET: TodoController/Create
        public ActionResult Create()
        {
            var newToDo = new CreateToDoDto
            {
                ToDoStatus = Const.TodoStatus.NotStarted,
                CreatedAt = DateTime.UtcNow.AddHours(5) // Adjusting for UTC+5 timezone
            };
            return View(newToDo);
        }

        // POST: TodoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateToDoDto newTodo)
        {
            var resultTodo = await _context.CreateTodoAsync(newTodo);
            if (resultTodo != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(newTodo);
        }

        // GET: TodoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            var updateToDoDto = new UpdateToDoDto
            {
                Id = todo.Id,
                Name = todo.Name,
                Description = todo.Description,
                ToDoStatus = todo.ToDoStatus,
            };
            return View(updateToDoDto);
        }

        // POST: TodoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateToDoDto updateTask)
        {
            if(id!= updateTask.Id)
            {
                ModelState.AddModelError("", "ID mismatch.");
                return View(updateTask);
            }
            var result = await _context.UpdateTodoAsync(id, updateTask);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to update ToDo.");
            return View(updateTask);
        }

        // GET: TodoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            return View(todo);
        }

        // POST: TodoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ReadToDoDto deletedDoto)
        {
            var result = _context.DeleteTodoAsync(id);
            if (result.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete ToDo.");
                return View(deletedDoto);
            }
        }
    }
}
