using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificateBuilder2.Models
{
    public class CertificateDBObject
    {
        public int Id { get; set; }
        public int? Coursekey { get; set; }
        public int UserGroupkey { get; set; }
        public string CertificateTemplate { get; set; }
    }
}