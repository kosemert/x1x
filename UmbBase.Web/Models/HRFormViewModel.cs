using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbBase.Web.Models
{
    public class HRFormViewModel
    {
        public string FullName{ get; set; }
        public string birthplace{ get; set; }
        public DateTime BirtDay{ get; set; }
        public string Sexual{ get; set; }
        public string maritalStatus{ get; set; }
        public string smoke{ get; set; }
        public string TelNo{ get; set; }
        public string Email{ get; set; }
        public string PreviewText{ get; set; }
        public HttpPostedFileBase file{ get; set; }
    }
}