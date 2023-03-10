using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FileRead
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string file = "genpact"+ new Random().Next().ToString()+".txt"; // ConfigurationManager.Configuration.GetSection("appSettings")["path"];
            try
            {
                // delete existing file , if any     
                Console.WriteLine("I am working");
                //Thread.Sleep(120000);
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

                Console.WriteLine("I am working");
                AmazonS3Client client = new AmazonS3Client();
                string bucketName = "genpact-dotnet-test";
                string objectName = file;
                string filePath = file;
                var x = await UploadFileAsync(client, bucketName, objectName, filePath);
            // Open the stream and read it back.    
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        public static async Task<bool> UploadFileAsync(IAmazonS3 client, string bucketName, string objectName, string filePath)
        {
            var request = new PutObjectRequest{BucketName = bucketName, Key = objectName, FilePath = filePath, };
            var response = await client.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Successfully uploaded {objectName} to {bucketName}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Could not upload {objectName} to {bucketName}.");
                return false;
            }
        }
    }
}