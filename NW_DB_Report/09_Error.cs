using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NW_DB_Report
{
    internal class Error
    {
        string Message { get; set; }
        public Error(string text = "")
        {
            Message = text; 
        }
    }
}
