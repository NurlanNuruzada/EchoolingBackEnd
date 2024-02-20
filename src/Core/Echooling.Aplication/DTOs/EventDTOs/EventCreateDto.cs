using Ecooling.Domain.Entites;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.DTOs.EventDTOs
{
    public class EventCreateDto
    {
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventFinishDate { get; set; }
        public decimal? Cost { get; set; }
        public string? orginazer { get; set; }
        public int? TotalSlot { get; set; }
        public string? Categoryname { get; set; }
        public string? Location { get; set; }
        public string? EventTitle { get; set; }
        public string? AboutEvent { get; set; }
        public IFormFile? image { get; set; }
        public Guid? EventCategoryiesId { get; set;}
    }
}
 