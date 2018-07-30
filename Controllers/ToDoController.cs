﻿using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCoreTodo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemService toDoItemService;

        public ToDoController(IToDoItemService itemService)
        {
            toDoItemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await toDoItemService.GetIncompleteItemsAsync();
            var model = new ToDoViewModel()
            {
                Items = items
            };

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(ToDoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await toDoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await toDoItemService.MarkDoneAsync(id);

            if (!successful)
            {
                return BadRequest("Could not mark the item as done");
            }

            return RedirectToAction("Index");
        }
    }
}