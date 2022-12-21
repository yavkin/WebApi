using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class RealtyCompany
    {
        [Column("RealtyCompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Realty Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")] public string Name { get; set; }
        [Required(ErrorMessage = "Realty Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte")]
        public string Location { get; set; }
        public string Country { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
