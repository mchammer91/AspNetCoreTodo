using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public interface IToDoItemService
    {
        Task<List<ToDoItem>> GetIncompleteItemsAsync();

        Task<bool> AddItemAsync(ToDoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);
    }
}
