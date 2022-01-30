using ToDoApp.Enums;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.Service;

public interface IToDoTaskService
{
    Task<ToDoTask> GetById(int id);
    Task<IEnumerable<ToDoTask>> GetALl(string title);
    Task Add(ToDoTaskVM toDoTaskVM);
    Task Delete(int id);
    Task Update(ToDoTaskVM toDoTaskVM);

}