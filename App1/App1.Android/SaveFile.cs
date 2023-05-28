using Android.Content;

using Java.IO;

using Shared;

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;

[assembly: Dependency(typeof(HakatonAPP.Droid.SaveFile))]
namespace HakatonAPP.Droid
{

    public class SaveFile:IFileEngine
    {
        public async Task<bool> WriteFile(string fileName, System.IO.Stream stream)
        {
            try
            {
                Context context = Android.App.Application.Context;
                

                string root = null;
                
                //Get the root path in android device.
                if (Android.OS.Environment.IsExternalStorageEmulated)
                {
                    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                    root = System.IO.Path.Combine(root,"Documents");
                }
                else
                    root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string path = System.IO.Path.Combine(root, fileName);
             
                Java.IO.File file = new Java.IO.File(path);

               
                //Remove if the file exists
                if (file.Exists()) file.Delete();

                //Write the stream into the file
                FileOutputStream outs = new FileOutputStream(file);
                byte[] buffer = new byte[4096];
               
                while (stream.Read(buffer,0,buffer.Length) > 0)
                {
                    outs.Write(buffer);
                }
                outs.Flush();
                outs.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}