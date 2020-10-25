using MultiplicationTable.Services;
using MultiplicationTable.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSave))]
namespace MultiplicationTable.UWP
{
    
    public class FileSave : IFileSave
    {
        public async Task<bool> SaveXml(string content,string filename)
        {
            StorageFolder picturesDirectory = KnownFolders.DocumentsLibrary;
            StorageFolder folderDirectory = picturesDirectory;

            // Get the folder or create it if necessary

            try
            {
                // Create the file.
                StorageFile storageFile = await folderDirectory.CreateFileAsync(filename,
                                                    CreationCollisionOption.ReplaceExisting);

                // Convert byte[] to Windows buffer and write it out.
                await FileIO.WriteTextAsync(storageFile, content);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
