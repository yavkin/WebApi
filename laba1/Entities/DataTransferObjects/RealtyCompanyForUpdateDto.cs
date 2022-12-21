using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class RealtyCompanyForUpdateDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public IEnumerable<ClientForCreationDto> Clients { get; set; }
    }
}
