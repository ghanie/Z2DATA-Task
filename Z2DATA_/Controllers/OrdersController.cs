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
        readonly DataAccessLayer _dataAccessLayer = new DataAccessLayer();

        [HttpGet]
        [Route("api/Orders/Index")]
        public IEnumerable<Orders> Index()
        {
            return _dataAccessLayer.GetOrders();
        }

        [HttpPost]
        [Route("api/Orders/Create")]
        public int Create(Process process)
        {
            return _dataAccessLayer.AddOrder(process);
        }

        /*[HttpGet]
        [Route("api/Orders/Details/{id}")]
        public Orders Details(int id)
        {
            return dataAccessLayer.PrintOrder(id);
        }*/

        [HttpPut]
        [Route("api/Orders/Edit")]
        public int Edit(Process process)
        {
            return _dataAccessLayer.UpdateOrder(process);
        }

        [HttpDelete]
        [Route("api/Orders/Delete/{id}")]
        public void Delete(int id)
        {
            _dataAccessLayer.DeleteOrder(id);
        }
    }
}
