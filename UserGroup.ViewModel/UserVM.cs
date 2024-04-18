using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<string> Groups { get; set; }
    }

    //public class UsersVM
    //{
    //    public List<UserVM> Users { get; set; }
    //    public List<UserVM> FilterUsers(string searchTerm)
    //    {
    //        return Users.Where(u => u.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    //    }
    //}
}
