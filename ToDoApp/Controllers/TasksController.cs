using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Service;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers;
[Authorize]
public class TasksController : Controller
{

    private readonly IToDoTaskService _service;

    public TasksController(IToDoTaskService service)
    {
        _service = service;
        
    }
    public async Task<IActionResult> Index(string title)
    {
        var tasks = await _service.GetALl( title);
        return View(tasks);
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(ToDoTaskVM toDoTaskVm)
    {
        if (!ModelState.IsValid) return View(toDoTaskVm);
        await _service.Add(toDoTaskVm);
        return RedirectToAction(nameof(Index));
    }
    
    
    
    
    
    public async Task<IActionResult> Update(int id )
    {
        var task = await _service.GetById(id);
        if (task == null) return View("NotFound");


        var updatedTask = new ToDoTaskVM()
        {
            Id=task.Id,
            Title = task.Title,
            Category = task.Category
        };
        return View(updatedTask);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(int id, ToDoTaskVM toDoTaskVm)
    {
        if (id != toDoTaskVm.Id) return View("NotFound");

        if (!ModelState.IsValid) return View(toDoTaskVm);
        await _service.Update(toDoTaskVm);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _service.GetById(id);
        if (task == null) return View("NotFound");
        return View(task);
    }
    
    
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        var task = await _service.GetById(id);
        if (task == null) return View("NotFound");
        await _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    
}