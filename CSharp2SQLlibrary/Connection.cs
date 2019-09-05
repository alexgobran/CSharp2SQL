using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SQLlibrary {
    public class Connection {

        
        public SqlConnection _Connection { get; set; } = null;

        public void Open() {
            this._Connection.Open();
            if (this._Connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection did not open!");
            }
        }

        //close connection when done with connection
        public void Close() {
            if(this._Connection.State != System.Data.ConnectionState.Open)
            {
                this._Connection.Close();
            }

        }

        //Constructer- paramaters require that you have to pass in a string server and  string database 

        //_Connection is a property - have to put underscore or change name because our Class is also called connection
        public Connection (string server, string database) {

            //Connection string - semicolon seperates key value pairs

            var ConnectionString = $"server={server};database={database};trusted_connection=true;";
            this._Connection = new SqlConnection(ConnectionString);
            

        }

           

        
    }
}
