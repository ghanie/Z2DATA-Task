using System;
using System.ComponentModel.DataAnnotations;

namespace Z2Data_.Models
{
    /* As Order is the view result of a process so the order class inherited the base properties from the process 
     * and have the following:
     * The id of the process which contain ( Customer Name, Process Type "Buy or Sell") 
     * The View for the Process Type
     * The view for the Goods Name as Part Name
     * The Calculated Order Cost Which is the Cost of (single item * Quantity)
     * The Order Date, which is assign automatically when the person place the order
     */
    public class Orders : Process
    {
        public int ProcessId { get; set; }
        public string ProcessType { get; set; }
        public string GoodsName { get; set; }
        public decimal OrderCost { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
