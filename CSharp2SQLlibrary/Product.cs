﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CSharp2SQLlibrary {
    public class Product {

        public static Connection Connection { get; set; }

        #region SQL statements
        private const string SqlGetAll = "SELECT * from Product";
        private const string SqlGetByPk = "SELECT * from Product Where Id = @Id";
        private const string SqlDelete = "DELETE from Products Where Id = @Id";
        private const string SqlInsert = "INSERT Products " +
            "(PartNbr ,Name, Price, Unit, PhotoPath, VendorId) " +
            "VALUES (@PartNbr ,@Name, @Price, @Unit, @PhotoPath, @VendorId)";
        private const string SqlUpdate = "UPDATE Products Set " +
            " PartNmbr= @PartNmbr, Name= @Name, Price = @Price, Unit = @Unit " +
            " PhotPath = @PhotPath, VendorId =@VendorId, Vendor=@Vendor";
        #endregion

        #region Instance Properties
        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotPath { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; private set; }
        #endregion

        //Collection of Instances

            public static Product GetByPk(int id) {
            var sqlcmd = new SqlCommand(SqlGetByPk, Connection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var product = new Product();
            LoadProductFromSql(product, reader);

            reader.Close();

            Vendor.Connection = Connection;
            var vendor = Vendor.SqlGetPK(product.VendorId);

            return product;
        }
        public static List<Product> GetAll() {
            var sqlcmd = new SqlCommand(SqlGetAll, Connection.sqlConnection);
            var reader = sqlcmd.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product();
                products.Add(product);
                LoadProductFromSql(product, reader);

            }

            reader.Close();
            Vendor.Connection = Connection;
            foreach (var prod in products)
            {
                var vendor = Vendor.SqlGetPK(prod.VendorId);
                prod.Vendor = vendor;
            }
            return products;
        }
        private static void LoadProductFromSql(Product product, SqlDataReader reader) {

            product.Id = (int)reader.GetInt32(reader.GetOrdinal("Id"));
            product.PartNbr = reader["PartNbr"].ToString();
            product.Name = reader["Name"].ToString();
            product.Price = (decimal)reader["Price"];
            product.Unit = reader["Unit"].ToString();
            product.PhotPath = reader["PhotoPath"]?.ToString();
            product.VendorId = (int)reader["VendorId"];

        }


        public override string ToString() {
            return $"Id={Id}, PartNmbr = {PartNbr}, Name = {Name}, Price = {Price}, " +
                $" Unit={Unit}, PhotPath = { PhotPath}, VendorId = { VendorId}, Vendor = {Vendor}";

        }

    }
}