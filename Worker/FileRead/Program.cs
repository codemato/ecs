using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace FileRead
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = ConfigurationManager.AppSettings["path"];

            try
            {
                // delete existing file , if any     
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                // Create a new file     
                using (FileStream fs = File.Create(file))
                {
                    // add text   
                    Byte[] firstline = new UTF8Encoding(true).GetBytes("first line");
                    fs.Write(firstline, 0, firstline.Length);
                    byte[] secondline = new UTF8Encoding(true).GetBytes("second line");
                    fs.Write(secondline, 0, secondline.Length);
                }

                // Open the stream and read it back.    
                using (StreamReader reader = File.OpenText(file))
                {
                    string s = "";
                    while ((s = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                        //Console.ReadLine();
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}
