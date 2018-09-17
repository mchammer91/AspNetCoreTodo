using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ManageUsersController(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await userManager.GetUsersInRoleAsync(Constants.AdministratorRole)).ToList();

            var everyone = await userManager.Users.ToListAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
    }
}