using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface IToDoItemService
    {
        Task<List<ToDoItem>> GetIncompleteItemsAsync(ApplicationUser user);

        Task<bool> AddItemAsync(ToDoItem newItem, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid id, ApplicationUser user);
    }
}
