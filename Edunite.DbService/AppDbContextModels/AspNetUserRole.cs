using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edunite.DbService.AppDbContextModels
{
    public partial class AspNetUserRole
    {
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual AspNetUser? User { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual AspNetRole? Role { get; set; }
    }
}
