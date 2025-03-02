using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrosCTe.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
        }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }


        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
