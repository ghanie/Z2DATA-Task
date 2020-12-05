using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Z2Data_.Models
{
    public class DataAccessLayer
    {
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Z2Data;Trusted_Connection=True;MultipleActiveResultSets=true";


        //To view all orders
        public IEnumerable<Orders> GetOrders()
        {

            var ordersList = new List<Orders>();

            using var connection = new SqlConnection(ConnectionString);
            // The Command var takes the stored procedure and the SQL connection
            var command = new SqlCommand("GetOrders", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            //Open the connection to Database
            connection.Open();

            //Create dataReader Variable
            var dataReader = command.ExecuteReader();

            // Start Reading from the InventoryProcess Database
            while (dataReader.Read())
            {

                var order = new Orders
                {
                    // Inventory Parameters
                    Id = Convert.ToInt32(dataReader["InventoryId"]),
                    OrderQuantity = Convert.ToInt32(dataReader["OrderQuantity"]),
                    OrderCost = Convert.ToDecimal(dataReader["OrderCost"]),
                    ProcessDate = Convert.ToDateTime(dataReader["InventoryProcessDate"]),

                    // Goods Parameters
                    GoodsId = Convert.ToInt32(dataReader["GoodsId"]),
                    GoodsName = dataReader["GoodsName"].ToString(),

                    // Process Parameters
                    ProcessId = Convert.ToInt32(dataReader["ProcessId"]),
                    CustomerName = dataReader["CustomerName"].ToString(),

                    // Process Type Parameters
                    ProcessTypeId = Convert.ToInt32(dataReader["ProcessTypeId"]),
                    ProcessType = dataReader["ProcessTypeName"].ToString()
                };

                // Add order to the list
                ordersList.Add(order);
            }

            //Close Connection 
            connection.Close();

            //Return the List
            return ordersList;
        }


        // Adding New Order To the System
        public int AddOrder(Process process)
        {

            using var connection = new SqlConnection(ConnectionString);

            var command = new SqlCommand("AddProcess", connection)
            {
                CommandType = CommandType.StoredProcedure
            };


            command.Parameters.AddWithValue("@CustomerName", process.CustomerName);
            command.Parameters.AddWithValue("@ProcessTypeId",process.ProcessTypeId);
            command.Parameters.AddWithValue("@GoodsId",process.GoodsId);
            command.Parameters.AddWithValue("@OrderQuantity", process.OrderQuantity);

            //Open The connection
            connection.Open();

            //Execute The Query
            command.ExecuteNonQuery();

            //Close The connection
            connection.Close();
            return 1;
        }

        //To Update the records of Orders  
        public int UpdateOrder(Process process)
        {
            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("UpdateProcess", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InventoryId", process.Id);
            command.Parameters.AddWithValue("@CustomerName", process.CustomerName);
            command.Parameters.AddWithValue("@ProcessTypeId", process.ProcessTypeId);
            command.Parameters.AddWithValue("@GoodsId", process.GoodsId);
            command.Parameters.AddWithValue("@OrderQuantity", process.OrderQuantity);


            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            return 1;
        }

        // Delete Particular Process From Records
        public void DeleteOrder(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("DeleteProcess", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InventoryId", id);


            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
