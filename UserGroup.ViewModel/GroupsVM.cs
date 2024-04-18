using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.ViewModel
{
    public class GroupVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalUsers { get; set; } = 0;
        public List<int> UserIds { get; set; }

    }

    //public class GroupsVM 
    //{
    //    public List<GroupVM> Groups { get; set; }
    //    public List<GroupVM> FilterUsers(string searchTerm)
    //    {
    //        return Groups.Where(u => u.GroupName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    //    }
    //}

}
