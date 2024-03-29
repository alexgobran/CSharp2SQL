﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SQLlibrary {
    public class Connection {

        
        public SqlConnection sqlConnection{ get; set; } = null;


        public void Open() {
            this.sqlConnection.Open();
            if (this.sqlConnection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection did not open!");
            }
        }

        //close connection when done with connection
        public void Close() {
            if(this.sqlConnection.State != System.Data.ConnectionState.Open)
            {
                this.sqlConnection.Close();
            }

        }

        //Constructer- paramaters require that you have to pass in a string server and  string database 

        //sqlConnectionis a property - have to put underscore or change name because our Class is also called connection
        public Connection (string server, string database) {

            //Connection string - semicolon seperates key value pairs

            var ConnectionString = $"server={server};database={database};trusted_connection=true;";
            this.sqlConnection= new SqlConnection(ConnectionString);
            

        }

           

        
    }
}
