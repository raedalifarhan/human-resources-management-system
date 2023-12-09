

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Branch : BaseEntity
    {
        [Required, MaxLength(50)]
        public string BranchName { get; set; }


        [MaxLength(256)]
        public string? Description { get; set; }

        public ICollection<Department>? Departments { get; set; }
    }
}
