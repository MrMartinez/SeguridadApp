using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class Singleton
    {
        private static Singleton instance = null;
        public string _connection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\MrMartinez\Documents\Developer\SeguridadApp\SeguridadAppBD.accdb";
       public Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
              
            }
     
        }
    }
}
