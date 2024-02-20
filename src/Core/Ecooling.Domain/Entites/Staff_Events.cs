namespace Ecooling.Domain.Entites
{
    public class Staff_Events:BaseEntity
    {
        public Guid StaffId { get; set; }
        public Staff staff { get; set; }
        public Guid eventsId { get; set; }
        public events events { get; set; }
    }
}
