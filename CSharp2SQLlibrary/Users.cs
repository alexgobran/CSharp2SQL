using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace CSharp2SQLlibrary {
    public class Users {

        public static Connection sqlConnection { get; set; }

        public static bool Update(Users user) {
            var sql = " UPDATE [User] set" +
               " Username = @Username," +
               " Password = @Password," +
               " FirstName = @FirstName," +
               " LastName = @LastName," +
               " Phone = @Phone," +
              " Email = @Email," +
                "IsAdmin = @IsAdmin," +
                "IsReviewer = @IsReviewer" +
                "Where Id = @Id";

            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            SetParameterValues(user, sqlcmd);
            sqlcmd.ExecuteNonQuery();
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;


        }

        private static void SetParameterValues(Users user, SqlCommand sqlcmd) {
            sqlcmd.Parameters.AddWithValue("@Username", user.Username);
            sqlcmd.Parameters.AddWithValue("@Password", user.Password);
            sqlcmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlcmd.Parameters.AddWithValue("@LastName", user.LastName);
            sqlcmd.Parameters.AddWithValue("@Phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@Email", user.Email);
            sqlcmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            sqlcmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);


        }
        const string SqlInsert = " Insert into [User]" +
                "(Username,Password, FirstName, LastName, Phone, Email, IsAdmin, IsReviewer)" +
                "VALUES" +
              "(@Username,@Password, @FirstName, @LastName, @Phone, @Email, @IsAdmin, @IsReviewer)";

        public static bool Insert(Users user) {
            var sql = SqlInsert;
            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            SetParameterValues(user, sqlcmd);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;

        }
        const string SQlDelete = " Delete from [User] where Id = @Id;";
        public static bool Delete(int id) {
            var sql = SQlDelete;
            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            sqlcmd.ExecuteNonQuery();
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return rowsAffected == 1;
            
        
        }
        
        public static Users Login(String username, string password) {

            var sql = "SELECT * from [User] Where Username = @Username AND Password = @Password";
            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Username", username);
            sqlcmd.Parameters.AddWithValue("@Password", password);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var user = new Users();
            LoadUserFromSql(user, reader);

            reader.Close();

            return user;

        }


        //Method

            public static Users GetByPk(int id) {

            var sql = "SELECT * from [User] Where Id = @Id";
            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id",id);
            var reader = sqlcmd.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            
            var user = new Users();
                

                

            reader.Close();

            return user;
            }
        public static List<Users> GetAll() {
            //sql statement
            var sql = "SELECT  * from [User];";
            //command
            var sqlcmd = new SqlCommand(sql, sqlConnection.sqlConnection);
            var reader = sqlcmd.ExecuteReader();
             var Users = new List<Users>();
                while(reader.Read())
            {
                
                var user = new Users();
                Users.Add(user);


                 LoadUserFromSql(user, reader);
                user.Id = (int)reader["Id"];
                user.Username = reader["Username"].ToString();
                user.Password = (string)reader["Password"];
                user.FirstName = (string)reader["FirstName"];
                user.LastName = (string)reader["LastName"];
                user.Phone = reader["Phone"]?.ToString();
                user.Email = reader["Email"]?.ToString(); //? if "Email" is not null turn it into a string, if it is null than make user.Email null
                user.IsAdmin = (bool)reader["IsAdmin"];
                user.IsReviewer = (bool)reader["IsReviewer"];

            }
            reader.Close();
            return Users;
        }

        private static void LoadUserFromSql(Users user, SqlDataReader reader) {
            user.Id = (int)reader["Id"];
            user.Username = reader["Username"].ToString();
            user.Password = (string)reader["Password"];
            user.FirstName = (string)reader["FirstName"];
            user.LastName = (string)reader["LastName"];
            user.Phone = reader["Phone"]?.ToString();
            user.Email = reader["Email"]?.ToString(); //? if "Email" is not null turn it into a string, if it is null than make user.Email null
            user.IsAdmin = (bool)reader["IsAdmin"];
            user.IsReviewer = (bool)reader["IsReviewer"];

        }

        public int Id { get; private set; }  // made the set private so nobody can set the Id
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } //? makes it nullable
        public bool IsReviewer { get; set; }

        public Users() {

        }

        public override string ToString() {
           
            
                return $"Id={Id}, Username={Username}, Password={Password}," +
                    $"Name={FirstName} {LastName},Admin?={IsAdmin}, Reviewer?={IsReviewer}";
            
        }
    }
}
