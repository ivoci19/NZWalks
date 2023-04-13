using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.DTO
{
    public class RegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

       
    }

    public class RegionViewDTO : RegionDTO
    {
        public Guid Id { get; set; }

    }


}
