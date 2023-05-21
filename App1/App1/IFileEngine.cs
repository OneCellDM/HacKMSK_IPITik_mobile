using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public interface IFileEngine
    {
       Task<bool> WriteFile(string filename, Stream data);
    }
}
