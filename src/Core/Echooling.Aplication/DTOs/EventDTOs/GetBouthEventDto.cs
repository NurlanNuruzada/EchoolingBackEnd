using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.EventDTOs
{
    public class GetBouthEventDto
    {
        public Guid GuId { get; set; }
        public virtual bool IsDeleted { get; set; }
        public string? EventTitle { get; set; }
        public string? Categoryname { get; set; }
        public string? Location { get; set; }

        public DateTime? EventStartDate { get; set; }
        public DateTime? EventFinishDate { get; set; }
    }
}
