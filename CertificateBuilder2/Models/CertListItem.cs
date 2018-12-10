using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CertificateBuilder2.Models
{
    public class CertListItem
    {
        public int Id { get; set; }
        public int? Coursekey { get; set; }
        public int UserGroupkey { get; set; }
        public string PageName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}