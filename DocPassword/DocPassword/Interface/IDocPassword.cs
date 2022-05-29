using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocPassword.Interface
{
    interface IDocPassword
    {
        bool AddPassword(string password, out string status);
        bool RemovePassword(string password, out string status);
    }
}
