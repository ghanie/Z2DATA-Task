using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Z2Data_.Models
{
    public class DataAccessLayer
    {
        // The connection string to the database 
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Z2Data;Trusted_Connection=True;MultipleActiveResultSets=true";


        /* This Function is to return all the orders from "InventoryProcess" Table in Database
         * From Stored Procedures "GetOrders"
         * */
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
                    OrderId = Convert.ToInt32(dataReader["InventoryId"]),
                    OrderQuantity = Convert.ToInt32(dataReader["OrderQuantity"]),
                    OrderCost = Convert.ToDecimal(dataReader["OrderCost"]),
                    OrderDate = Convert.ToDateTime(dataReader["InventoryProcessDate"]),

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


        /* This Function is Creating New Order From Stored Procedures "AddProcess"
         * 1- By adding "New Process" which contains CustomerName an ProcessTypeId in Process Table
         * 2- Insert ProcessId into the InventoryProcess Table + GoodsId + Quantity
         * 3- OrderDate Added Automatically By GETDATE()
         * 4- OrderCost is Calculated by fetching the goodsId price * quantity,
         * Then Updated in the InventoryProcess Table
         * 5- Update the Goods Inventory Count by Subtracting The Order Quantity
         */
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

         // This Function is Updating Order From Stored Procedures "UpdateProcess"
         // 1- Passing The InventoryId to know which Order to update
         // 2- Update CustomerName an ProcessTypeId in Process Table
         // 2- Update GoodsId + Quantity
         // 3- OrderDate Added Automatically By GETDATE()
         // 4- OrderCost is recalculated by fetching the goodsId price * quantity,
         // Then Updated in the InventoryProcess Table
         // Note: if the goodsId changed I couldn't restore the quantity to the original,
         // I'll search for it how can I modify 
         public int UpdateOrder(Process process)
        {
            using var connection = new SqlConnection(ConnectionString);
            var command = new SqlCommand("UpdateProcess", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@InventoryId", process.OrderId);
            command.Parameters.AddWithValue("@CustomerName", process.CustomerName);
            command.Parameters.AddWithValue("@ProcessTypeId", process.ProcessTypeId);
            command.Parameters.AddWithValue("@GoodsId", process.GoodsId);
            command.Parameters.AddWithValue("@OrderQuantity", process.OrderQuantity);


            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            return 1;
        }

        // This Function is Deleting Order From Stored Procedures "DeleteProcess"
        // 1- Passing The ProcessId to local variable x
        // 2- Passing The InventoryId to know which Order to Delete from InventoryProcess Table
        // 3- take the x variable to delete the Process From Process Table too.
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
