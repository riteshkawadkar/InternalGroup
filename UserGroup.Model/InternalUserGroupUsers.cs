using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroup.Model
{
    public class InternalUserGroupUsers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int InternalUserGroupId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("InternalUserGroupId")]
        public InternalUserGroup InternalUserGroup { get; set; }
    }
}
