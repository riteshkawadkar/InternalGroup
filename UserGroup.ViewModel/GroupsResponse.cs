using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.ViewModel
{
    public class GroupsResponse
    {
        public string PartialViewHtml { get; set; }
        public List<int> UserIds { get; set; }
    }
}
