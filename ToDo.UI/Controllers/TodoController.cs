using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.UI.Const;
using ToDo.UI.DTOs.TodoDto;
using ToDo.UI.Service;
using X.PagedList.Extensions;



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
        public async Task<ActionResult> Index(string? searchString=null, string? sortOrder=null, TodoStatus? selectedStatus=null)
        {
            var todos = await _context.GetAllTodosAsync(searchString);
            var countTodos = todos.Count();

            ViewBag.Status = new SelectList(Enum.GetValues(typeof(TodoStatus)),selectedStatus
                );
                
            ViewData["CurrentFilter"] = searchString;
            ViewData["SortName"] = sortOrder == "name_desc" ? "name_asc" : "name_desc";
            ViewData["SortDescription"] = sortOrder == "des_desc" ? "des_asc" : "des_desc";
            ViewBag.SortDate = sortOrder == "date_desc" ? "date_asc" : "date_desc";
            ViewBag.SortProgress = sortOrder == "prog_desc" ? "prog_asc" : "prog_desc";
            //filter
            if(selectedStatus.HasValue)
            {
                 todos = todos.Where(f => f.ToDoStatus == selectedStatus.Value).ToList();
            }
            
            todos = sortOrder switch
            {
                "name_desc" => todos.OrderByDescending(x => x.Name).ToList(),
                "name_asc" => todos.OrderBy(x => x.Name).ToList(),

                "des_desc" => todos.OrderByDescending(x => x.Description).ToList(),
                "des_asc" => todos.OrderBy(x => x.Description).ToList(),

                "date_desc"=>todos.OrderByDescending(x=>x.CreatedAt).ToList(),
                "date_asc"=>todos.OrderBy(x=>x.CreatedAt).ToList(),

                "prog_desc"=>todos.OrderByDescending(x=>x.Progress).ToList(),
                "prog_asc"=>todos.OrderBy(x=>x.Progress).ToList(),

                _ =>todos.OrderBy(n=>n.Name).ToList()

            };

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
