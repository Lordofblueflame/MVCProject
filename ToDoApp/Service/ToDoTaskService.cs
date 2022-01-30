using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Database;
using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.Service;

public class ToDoTaskService:IToDoTaskService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public ToDoTaskService(AppDbContext context, IHttpContextAccessor accessor, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _httpContextAccessor = accessor;
        _userManager = userManager;
    }
    
    public  async Task<ToDoTask> GetById(int id)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        
        var task = await _context.ToDoTasks.FirstOrDefaultAsync(t => (t.Id == id) && (t.Owner== currentUser));
        return task;
    }


    public async Task<IEnumerable<ToDoTask>> GetALl(string title)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        IQueryable<ToDoTask> tasksQuery = _context.ToDoTasks;
        tasksQuery = tasksQuery.Where(t => t.Owner == currentUser);
        
        if (!string.IsNullOrEmpty(title))
        {
            tasksQuery = tasksQuery.Where(x => x.Title.Contains(title));
        }

        var tasks = await tasksQuery.ToListAsync();
        return tasks;
    }


    public async Task Add(ToDoTaskVM toDoTaskVm)
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        var newTask = new ToDoTask()
        {
            Title = toDoTaskVm.Title,
            Category = toDoTaskVm.Category,
            Owner = currentUser
        };
        await _context.ToDoTasks.AddAsync(newTask);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var deletedTask = await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
        _context.ToDoTasks.Remove(deletedTask);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ToDoTaskVM toDoTaskVM)
    {
        var dbTask = await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == toDoTaskVM.Id);
        if (dbTask != null)
        {
            dbTask.Title = toDoTaskVM.Title;
            dbTask.Category = toDoTaskVM.Category;
        }

        await _context.SaveChangesAsync();
    }
    
}