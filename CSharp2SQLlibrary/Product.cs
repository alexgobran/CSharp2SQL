using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CSharp2SQLlibrary {
    public class Product {

        public static Connection Connection { get; set; }

        #region SQL statements
        private const string SqlGetAll = "SELECT * from Product";
        private const string SqlGetByPk = "SELECT * from Product Where Id = @Id";
        private const string SqlGetByPartNbr = SqlGetAll + " Where PartNbr =  @PartNbr";
        private const string SqlGetbyCode = SqlGetAll + " join Vendor on Product.VendorId = Vendor.Id where Code = @Code";
        private const string SqlDelete = "DELETE from Product Where Id = @Id";
        private const string SqlInsert = "INSERT Product " +
            "(PartNbr ,Name, Price, Unit, PhotoPath, VendorId) " +
            "VALUES (@PartNbr ,@Name, @Price, @Unit, @PhotoPath, @VendorId)";
        private const string SqlUpdate = " UPDATE Products Set " +
            " PartNbr = @PartNbr, Name= @Name, Price = @Price, Unit = @Unit " +
            " PhotoPath = @PhotoPath, VendorId = @VendorId, Vendor =@VendorId" +
            "Where Id = @Id";
        #endregion


        public static Product GetByPartNbr(string partNbr) {
            var sqlcmd = new SqlCommand(SqlGetByPartNbr, Connection.sqlConnection);
           sqlcmd.Parameters.AddWithValue("@PartNbr", partNbr);
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
            return product;


            public static Product SqlGet(string partNbr) {
                var sqlcmd = new SqlCommand(SqlGetProdByVen, Connection.sqlConnection);
                sqlcmd.Parameters.AddWithValue("@PartNbr", partNbr);
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
                return product;

            }
        public static bool Insert(Product product) {
            var sqlcmd = new SqlCommand(SqlInsert, Connection.sqlConnection);
            SetParameterValues(product, sqlcmd);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;
         


        }

        public static bool Update(Product product) {
            var sqlcmd = new SqlCommand(SqlUpdate, Connection.sqlConnection);
            SetParameterValues(product, sqlcmd);
            sqlcmd.Parameters.AddWithValue("@Id", product.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        public static bool Delete(int id) {
            var sqlcmd = new SqlCommand(SqlDelete, Connection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;

        }
        public static bool Delete(Product product) {
            return Delete(product.Id);
        }
        private static void SetParameterValues(Product product, SqlCommand sqlcmd) {
            sqlcmd.Parameters.AddWithValue("@PartNbr", product.PartNbr);
            sqlcmd.Parameters.AddWithValue("@Name", product.Name);
            sqlcmd.Parameters.AddWithValue("@Price", product.Price);
            sqlcmd.Parameters.AddWithValue("@Unit", product.Unit);
            sqlcmd.Parameters.AddWithValue("@PhotoPath", (object)product.PhotoPath?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@VendorId", product.VendorId);
           
        }
        
       
        #region Instance Properties
      private int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
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
            product.PhotoPath = reader["PhotoPath"]?.ToString();
            product.VendorId = (int)reader["VendorId"];

        }


        public override string ToString() {
            return $"Id={Id}, PartNbr = {PartNbr}, Name = {Name}, Price = {Price}, " +
                $" Unit={Unit}, PhotoPath = { PhotoPath}, VendorId = { VendorId}, Vendor = {Vendor}";

        }

    }
}