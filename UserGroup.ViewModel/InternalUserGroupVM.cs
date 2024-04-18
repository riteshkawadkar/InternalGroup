using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.ViewModel
{
    public class InternalUserGroupVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Type { get; set; }

        public List<int> UserIds { get; set; }

    }
}
