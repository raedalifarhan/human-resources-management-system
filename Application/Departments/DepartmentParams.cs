using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departments
{
    public class DepartmentParams : PagingParams
    {
        public string? BranchId { get; set; }
    }
}
