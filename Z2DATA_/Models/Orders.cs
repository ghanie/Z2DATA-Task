using System;
using System.ComponentModel.DataAnnotations;

namespace Z2Data_.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        // Customer Name
        public string CustomerName { get; set; }

        public int ProcessId { get; set; }

        public string GoodsName { get; set; }

        public int GoodsId { get; set; }

        public int OrderQuantity { get; set; }

        public decimal OrderCost { get; set; }

        public string ProcessType { get; set; }

        public int ProcessTypeId { get; set; }

        public DateTime ProcessDate { get; set; }
    }
}
