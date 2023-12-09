
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Department : BaseEntity
    {
        [DisplayName("Department Name"), MaxLength(50)]
        public string DepartmentName { get; set; } 
            = string.Empty;


        [DisplayName("Department Description"), MaxLength(256)]
        public string? Description { get; set; }


        [DisplayName("Branch")]
        public Guid? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public Branch? Branch { get; set; }
      

        public ICollection<Employee>? Employees { get; }
    }
}
