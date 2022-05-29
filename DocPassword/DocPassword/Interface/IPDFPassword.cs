using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocPassword.Interface
{
    interface IPDFPassword
    {
        bool AddPDFPassword(string password, out string status);
        bool RemovePDFPassword(string password, out string status);
    }
}
