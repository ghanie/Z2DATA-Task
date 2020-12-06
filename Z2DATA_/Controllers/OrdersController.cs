using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Z2Data_.Models;

namespace Z2Data_.Controllers
{

    public class OrdersController : Controller
    {
        //Create a dataAccessLayer Object to handle the data 
        private readonly DataAccessLayer _dataAccessLayer = new DataAccessLayer();


        // API to pass the Orders to the web page
        [HttpGet]
        [Route("api/Orders/Index")]
        public IEnumerable<Orders> Index()
        {
            return _dataAccessLayer.GetOrders();
        }


        // API to pass the new order to the database
        [HttpPost]
        [Route("api/Orders/Create")]
        public int Create(Process process)
        {
            return _dataAccessLayer.AddOrder(process);
        }



        // API to pass the updated order to the database
        [HttpPut]
        [Route("api/Orders/Edit")]
        public int Edit(Process process)
        {
            return _dataAccessLayer.UpdateOrder(process);
        }


        // API to delete an order by passing the id
        [HttpDelete]
        [Route("api/Orders/Delete/{id}")]
        public void Delete(int id)
        {
            _dataAccessLayer.DeleteOrder(id);
        }
    }
}
