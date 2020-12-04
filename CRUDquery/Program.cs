using CRUDquery.Classes;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CRUDquery
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            string connection;

            using (FileStream fs = new FileStream(@"Files\ConnString.txt", FileMode.Open, FileAccess.ReadWrite))
            {
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string));
                connection = dcjs.ReadObject(fs) as string;
            }


            Menu.StartMenu(connection);
        }
    }
}
