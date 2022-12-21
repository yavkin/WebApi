using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Client
    {
        [Column("ClientId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Client name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")] public string Name { get; set; }
        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        [ForeignKey(nameof(RealtyCompany))]
        public Guid RealtyCompanyId { get; set; }
        public RealtyCompany RealtyCompany { get; set; }
    }
}
