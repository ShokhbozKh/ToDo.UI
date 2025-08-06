using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.UI.DTOs.TodoDto;
using ToDo.UI.Service;

namespace ToDo.UI.Controllers
{
    public class TodosController : Controller
    {
        private readonly IToDoService _context;
        public TodosController(IToDoService toDoService)
        {
            _context = toDoService ??
                throw new ArgumentNullException(nameof(toDoService));
        }
        public async Task<ActionResult> Index(string? searchString, string sortOrder)
        {
            var todos = await _context.GetAllTodosAsync(searchString);
            var countTodos = todos.Count();
            ViewData["CurrentFilter"] = searchString;
            ViewBag.SearchResult = countTodos;
            ViewData["nameSortParm"] = string.IsNullOrWhiteSpace(sortOrder) ? "desc":"asc";
            switch(sortOrder)
            {
                case "desc":
                    todos = todos.OrderByDescending(t => t.Name);
                    break;
                case "asc":
                    todos = todos.OrderBy(t => t.Name);
                    break;
                default:
                    todos = todos.OrderBy(t => t.Name);
                    break;
            }
            return View(todos);
        }
        public async Task<ActionResult> Details(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        public ActionResult Create()
        {
            
            var newToDoDto = new CreateToDoDto
            {
                ToDoStatus = Const.TodoStatus.NotStarted,
            };
            return View(newToDoDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateToDoDto newToDoDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdToDo = await _context.CreateTodoAsync(newToDoDto);
                    if (createdToDo != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", "Failed to create ToDo.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(newToDoDto);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateToDoDto updateToDoDto)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedToDo = await _context.UpdateTodoAsync(id, updateToDoDto);
                    if (updatedToDo != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", "Failed to update ToDo.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }
            return View(updateToDoDto);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ReadToDoDto delete)
        {
            var todo = await _context.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }   
            try
            {
                var isDeleted = await _context.DeleteTodoAsync(id);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to delete ToDo.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View(todo);
        }
    }
}
