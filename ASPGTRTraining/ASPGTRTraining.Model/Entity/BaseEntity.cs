using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.Model.Entity
{
    public class BaseEntity
    {
        public String Id { get; set; } = Guid.NewGuid().ToString();
       
        public bool IsDeleted { get; set; }

        public string UpBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

    }
}
