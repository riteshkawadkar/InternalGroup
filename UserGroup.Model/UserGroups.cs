using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.Model
{
    public class UserGroups
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [ForeignKey("UserId")]
    
        public User User { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
