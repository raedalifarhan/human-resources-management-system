using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Branches
{
    public class BranchCommandDto
    {

        [Required, MaxLength(50)]
        public string BranchName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string? Description { get; set; }
    }
}
