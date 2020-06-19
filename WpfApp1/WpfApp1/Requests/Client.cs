using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WpfApp1.Requests
{
    public class Client
    {
       
        
        public string FullName { get; set; }
        [Key]
        public string PassID { get; set; }
        public int Debt { get; set; }
    }
}