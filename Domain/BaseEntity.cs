using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string? CreateDate { get; set; }

        [MaxLength(50)]
        public string? LastUpdateDate { get; set; }
    }
}