using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificateBuilder2.Models
{
    public class CertificateModel
    {
        public int? Id { get; set; }
        public int? Coursekey { get; set; }
        public int UserGroupkey { get; set; }
        public string Page_name { get; set; }
        public string Orientation { get; set; }
        public string Image_src { get; set; }
        public CertificateElementModel Greet { get; set; }
        public CertificateElementModel Fullname { get; set; }
        public CertificateElementModel Course { get; set; }
        public CertificateElementModel Completed { get; set; }
        public CertificateElementModel Duration { get; set; }
        public CertificateElementModel Statement1 { get; set; }
        public CertificateElementModel Statement2 { get; set; }
        public CertificateElementModel Statement3 { get; set; }
        public CertificateElementModel Statement4 { get; set; }
    }
}