using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificateBuilder2.Models
{
    public class CertificateElementModel
    {
        public string XMLId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Font_Type { get; set; }
        public int Font_Size { get; set; }
        public string Font_Style { get; set; }
        public int Color_Alpha { get; set; }
        public int Color_Red { get; set; }
        public int Color_Green { get; set; }
        public int Color_Blue { get; set; }
        public string Text_Align { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public string Text { get; set; }
        public bool Text_is_static { get; set; }
        public bool Disabled { get; set; }
    }
}