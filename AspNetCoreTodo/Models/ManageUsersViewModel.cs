using System.Collections.Generic;

namespace AspNetCoreTodo.Models
{
    public class ManageUsersViewModel
    {
        public List<ApplicationUser> Administrators { get; set; }
        public List<ApplicationUser> Everyone { get; set; }
    }
}
