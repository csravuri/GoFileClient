using System;
using System.Collections.Generic;
using System.Text;

namespace GoFileClient.Entities
{
    public class Entity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime LastUpdatedDate
        {
            get
            {
                return ModifiedDate.HasValue ? ModifiedDate.Value : CreatedDate;
            }
        }



    }
}
