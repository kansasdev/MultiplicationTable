using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using MultiplicationTable.Droid;
using MultiplicationTable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSave))]
namespace MultiplicationTable.Droid
{
    public class FileSave : IFileSave
    {
        public async Task<bool> SaveXml(string content, string filename)
        {
            try
            {
                File picturesDirectory = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
                File folderDirectory = picturesDirectory;
                
                using (File xmlFile = new File(folderDirectory, filename))
                {
                    xmlFile.CreateNewFile();
                    using (FileOutputStream outputStream = new FileOutputStream(xmlFile))
                    {
                        await outputStream.WriteAsync(UTF8Encoding.UTF8.GetBytes(content));
                    }
                    // Make sure it shows up in the Photos gallery promptly.
                    MediaScannerConnection.ScanFile(MainActivity.Instance,
                                                    new string[] { xmlFile.Path },
                                                    new string[] { "application/xml" }, null);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return true;
        }
    }
}