using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        public DateTime CreateData { get; set; }
        public DateTime? UpdateData { get; set; }
        public DateTime? DeleteData { get; set; }
    }
}
