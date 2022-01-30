using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ToDoApp.Enums;

namespace ToDoApp.Models;

public class ToDoTask
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public Category Category { get; set; }
    
    public IdentityUser Owner { get; set; }
}

