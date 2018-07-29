using AspNetCoreTodo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public class FakeToDoItemService : IToDoItemService
    {
        public Task<List<ToDoItem>> GetIncompleteItemsAsync()
        {
            var item1 = new ToDoItem
            {
                Title = "Learn ASP.NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item2 = new ToDoItem
            {
                Title = "Build Awesome Apps",
                DueAt = DateTimeOffset.Now.AddDays(3)
            };

            return Task.FromResult(new List<ToDoItem> { item1, item2 });
        }

        public async Task<bool> AddItemAsync(ToDoItem newItem)
        {
            var item = new ToDoItem
            {
                Id = Guid.NewGuid(),
                DueAt = newItem.DueAt,
                IsDone = newItem.IsDone,
                Title = newItem.Title
            };

            return await Task.FromResult(true);
        }
    }
}
