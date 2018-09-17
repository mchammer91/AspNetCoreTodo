using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ToDoItemService(ApplicationDbContext dbContext)
        {
            applicationDbContext = dbContext;
        }

        public async Task<List<ToDoItem>> GetIncompleteItemsAsync(ApplicationUser user)
        {
            return await applicationDbContext.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<bool> AddItemAsync(ToDoItem newItem, ApplicationUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            newItem.UserId = user.Id;

            applicationDbContext.Items.Add(newItem);

            var saveResult = await applicationDbContext.SaveChangesAsync();

            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await applicationDbContext.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null)
            {
                return false;
            }

            item.IsDone = true;

            var saveResult = await applicationDbContext.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
