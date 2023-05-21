using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    class FileEngine : IFileEngine
    {
        IFileEngine fileEngine = DependencyService.Get<IFileEngine>();

        public Task<bool> WriteFile(string filename, Stream data)
        {
           
           return  fileEngine.WriteFile(filename, data);
        }
    }
}
