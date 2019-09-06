using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CSharp2SQLlibrary {
    public class Vendor {



        //Variables
        public static Connection Connection { get; set; }

        private const string SqlGetAll = "SELECT * from Vendor;";

        private const string SqlGetByPk = SqlGetAll + "where Id = @Id";

        private const string SqlDelete = "Delete From Vendor where Id = @Id";

        private const string SqlUpdate = " Update Vendor Set" +
              " Code = @Code," +
               " Address = @Address," +
               " Name = @Name," +
               " City = @City," +
               " State = @State," +
               " Zip = @Zip," +
               " Phone = @Phone," +
              " Email = @Email," +
               " Phone = @Phone," +
              " Email = @Email," +
            "Where Id = @Id";
        private const string SqlInsert = "INSERT into Vendor" +
            "Code, Name, Address, City, State, Zip, Phone, Email)" +
            "VALUES (Code, Name, @Address, @City, @State, @Zip, @Phone, @Email)";

        public static List<Vendor> GetAll() {
            var sqlcmd = new SqlCommand(SqlGetAll, Connection.sqlConnection);
            var reader = sqlcmd.ExecuteReader();
            var vendor = new List<Vendor>();
            while (reader.Read())
            {
                var vendor = new Vendor();
                vendor.Add(vendor);
                LoadVendorFromSql(vendor, reader);
            }
            reader.Close();
            return vendor;
        }

        const string SQlDelete = " Delete from [Vendor] where Id = @Id;";
        public static bool Delete(int id) {
            var sql = SQlDelete;
            var sqlcmd = new SqlCommand(sql, Connection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            sqlcmd.ExecuteNonQuery();
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;


        }

        public static Vendor SqlGetPK (int Id) {
            var sql = "SELECT * from [Vendor] Where Id = @Id";
            var sqlcmd = new SqlCommand(sql, Connection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id",Id);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var vendor = new Vendor();
            LoadVendorFromSql(vendor, reader);




            reader.Close();

            return vendor;
        }
         


           static void LoadVendorFromSql(Vendor vendor, SqlDataReader reader) {
                vendor.Id = (int)reader["Id"];
                vendor.Code = reader["Code"].ToString();
                vendor.Address = reader["Address"].ToString();
                vendor.City = reader["City"].ToString();
                vendor.State = reader["State"].ToString();
                vendor.Zip = reader["Zip"]?.ToString();
                vendor.Phone = reader["Phone"]?.ToString();
                vendor.Email = reader["Email"]?.ToString();
            }


        //Properties
         public int Id { get; private set; }  
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
      
        public string Zip { get; set; } 
        public string Phone { get; set; }
        public string Email { get; set; }


        public Vendor() {

        }
        public override string ToString() {


            return $"Id={Id}, Code={Code}, Name={Name}," +
                $"Address={Address},City = {City},State={State}, Zip={Zip}, Phone = {Phone}, Email = {Email}";
        }




    }
}

