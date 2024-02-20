using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecooling.Domain.Entites
{
    public class Logger:BaseEntity
    {
        public string UserId { get; set; }
        public DateTime? ActionTime { get; set; }
        public string ActiondEntityName { get; set; }
        public string ActiondEntityId { get; set; }
    }
}
