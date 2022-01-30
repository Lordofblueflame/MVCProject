using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;

namespace ToDoApp.ViewModels;

public class ToDoTaskVM
{
    public int Id { get; set; }
    
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Display(Name = "Category")]
    [Required(ErrorMessage = "Category is required")]
    public Category Category { get; set; }
}