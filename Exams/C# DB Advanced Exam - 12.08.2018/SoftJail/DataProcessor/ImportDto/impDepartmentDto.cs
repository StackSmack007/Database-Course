using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public  class impDepartmentDto
    {
        [Required, MinLength(3), MaxLength(25)]
        public string Name { get; set; }
        public virtual ICollection<impCellDto> Cells { get; set; } = new List<impCellDto>();
    }
}
