﻿using System;
using CSharp2SQLlibrary;        
using System.Diagnostics;


namespace CSharp2SQL {


    class Program {
        public static void Main(string[] args) {
            RunProductTest();

        }
       public static void RunProductTest() {



            var conn = new Connection(@"localhost\sqlexpress", "PRS");
            conn.Open();
            Product.Connection = conn;

           

            var Computer = Product.GetByPk(5);
            Console.WriteLine(Computer);
          

            //var products = Product.GetAll();
            //foreach (var p in products)
            //{
            //    Console.WriteLine(p);
            //}

            conn.Close();
           


        }
            void RunVendorsTest() {



            //var conn = new Connection(@"localhost\sqlexpress", "PRS");
            //conn.Open();
            //Vendor.Connection= conn;

            //var vendors = Vendor.GetAll();
            //foreach(var v in vendors)
            //{
            //    Console.WriteLine(v.Name);
            //}



            //var vendor = Vendor.SqlGetPK(2);
            //Debug.WriteLine(vendor);
            //var vendornf = Vendor.SqlGetPK(222);
            //var success = Vendor.SqlDelete(4);
            //var vendor3 = Vendor.SqlGetPK(3);
            //Debug.WriteLine(vendor);



            //var vendordell= Vendor.SqlGetPK(2);
            //vendordell.Name = "";
            //vendordell.Name = "King";
            //    success = Users.Update(userabc);
        }



        //public static void Main(string[] args) {




        //    var conn = new Connection(@"localhost\sqlexpress", "PRS");
        //    conn.Open();
        //    Users.sqlConnection = conn;
        //    var userLogin = Users.Login("sa", "sa");
        //    Console.WriteLine(userLogin);

            //var userFailedLogin = Users.Login("xx", "xx");
            //Console.WriteLine(userFailedLogin?.ToString()?? "not found");
            //var users = Users.GetAll();
            //foreach (var u in users)
            //{
            //    Console.WriteLine(u);
            //}
            //var user = Users.GetByPk(2);
            //Debug.WriteLine(user);
            //var usernf = Users.GetByPk(222);
            //var success = Users.Delete(4);
            //var user3 = Users.GetByPk(3);
            //Debug.WriteLine(user3);


            //    var newuser = new Users();
            //    newuser.Username = "ccc";
            //    newuser.Password = "433";
            //    newuser.FirstName = "Normal";
            //    newuser.LastName = "Not";
            //newuser.Phone = "513-455-2231";
            //newuser.Email = "alee@gmail.com";
            //newuser.IsAdmin = false;
            //    newuser.IsReviewer = true;
            //    success = Users.Insert(newuser);

            //var userabc = Users.GetByPk(13);
            //userabc.FirstName = "The";
            //userabc.LastName = "King";
            //    success = Users.Update(userabc);

            //    conn.Close();


        //    var pgm = new Program();
        //    pgm.Run();

        //}

        //public void Run() {
            
        //}




        // if you ever want to put a \ in a normal string you have to putt

    }

   
}
