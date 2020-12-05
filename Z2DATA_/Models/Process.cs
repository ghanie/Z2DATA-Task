using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Z2Data_.Models
{
    public class Process
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }

        public int ProcessTypeId { get; set; }

        public int GoodsId { get; set; }

        public int OrderQuantity { get; set; }
    }
}
