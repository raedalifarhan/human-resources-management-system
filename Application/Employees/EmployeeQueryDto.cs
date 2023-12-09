using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Employees
{
    public class EmployeeQueryDto : BaseEntity
    {

        [MaxLength(50)]
        public string DepartmentName { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? Description { get; set; }

        public Guid? BranchId { get; set; }
    }
}
