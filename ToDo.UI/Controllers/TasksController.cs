using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.UI.Const;
using ToDo.UI.DTOs.TaskDto;
using ToDo.UI.Models;
using ToDo.UI.Service;

namespace ToDo.UI.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _context;
        private readonly IToDoService _toDoService;
        public TasksController(ITaskService taskService, IToDoService toDoService)
        {
            _context = taskService ??
                throw new ArgumentNullException(nameof(taskService));
            _toDoService = toDoService;
        }
        // GET: TasksController
        public async Task<ActionResult> Index()
        {
            var tasks = await _context.GetAllTasksAsync();
            if (tasks == null || !tasks.Any())
            {
                return View(new List<ReadTaskDto>());
            }
            return View(tasks);
        }

        // GET: TasksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var task = await _context.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // GET: TasksController/Create
        public async Task<ActionResult> Create()
        {
            var newTaskDto = new CreateTaskDto
            {
                DurationInMinutes = 0,
            };
            var todos = await _toDoService.GetAllTodosAsync();
            ViewBag.TodoList = new SelectList(todos, "Id", "Name");
            return View(newTaskDto);
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTaskDto newTask)
        {
            var createdTask = await _context.CreateTaskAsync(newTask);
            if (createdTask != null)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to create Task.");
            return View(newTask);
        }

        // GET: TasksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var task = await _context.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var todoName = await _toDoService.GetTodoByIdAsync(id);
           
            var todos = await _toDoService.GetAllTodosAsync();
            ViewBag.TodoNames = new SelectList(todos, "Id","Name", id);
            return View(task);
        }

        // POST: TasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, UpdateTaskDto updateTask)
        {
            var updated = await _context.UpdateTaskAsync(id, updateTask);
            if( updated != null)
            {
                return RedirectToAction(nameof(Index));
            }
            var todos = await _toDoService.GetAllTodosAsync();
            ViewBag.TodoNames = new SelectList(todos, "Id", "Name");

            ModelState.AddModelError("", "Failed to update Task.");
            return View(updateTask);
        }

        // GET: TasksController/Delete/5
        public async Task< ActionResult> Delete(int id)
        {
            var task = await _context.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: TasksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ReadTaskDto deleteTask)
        {
           var deleted = _context.DeleteTaskAsync(id);
            if (deleted.Result)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete Task.");
            return View(deleteTask);
        }
    }
}
