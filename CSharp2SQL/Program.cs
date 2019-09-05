using System;
using CSharp2SQLlibrary;        
using System.Diagnostics;


namespace CSharp2SQL {


    class Program {

        
        public static void Main(string[] args) {


       

            var conn = new Connection(@"localhost\sqlexpress", "PRS");
            conn.Open();
            Users._Connection = conn;
            var userLogin = Users.Login("sa", "sa");
            Console.WriteLine(userLogin);

            var userFailedLogin = Users.Login("xx", "xx");
            Console.WriteLine(userFailedLogin?.ToString()?? "not found");
            var users = Users.GetAll();
            foreach (var u in users)
            {
                Console.WriteLine(u);
            }
            var user = Users.GetByPk(222);
            Debug.WriteLine(user);
            conn.Close();


            var pgm = new Program();
            pgm.Run();

        }

        private void Run() {
            
        }




        // if you ever want to put a \ in a normal string you have to putt

    }

   
}
