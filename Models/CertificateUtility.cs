using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CertificateBuilder2.Models
{
    public class CertificateUtility
    {
        public static CertificateModel GetCertificate(int CertId)
        {
            if (CertId == 0)
            {
                return NewCertificate();
            }

            CertificateDBObject CertDBOject = new CertificateDBObject();

            using (TestDbContext db = new TestDbContext())
            {
                SqlParameter p = new SqlParameter()
                {
                    ParameterName = "CertId",
                    SqlDbType = SqlDbType.Int,
                    Value = CertId
                };

                CertDBOject = db.Database.SqlQuery<CertificateDBObject>("Exec GetExisitingCertificate @CertId", p).FirstOrDefault();
            }


            //Populate CertificateModel with db data
            CertificateModel existingCert = new CertificateModel()
            {
                Id = CertDBOject.Id,
                Coursekey = CertDBOject.Coursekey,
                UserGroupkey = CertDBOject.UserGroupkey
            };

            XDocument certXML = XDocument.Parse(CertDBOject.CertificateTemplate);

            existingCert.Page_name = certXML.Root.Element("page").Attribute("name").Value;
            existingCert.Orientation = certXML.Root.Element("page").Attribute("orientation").Value;
            existingCert.Image_src = certXML.Root.Element("page").Element("image").Attribute("src").Value;

            // Parse the XMl and convert to CertificateElementModel
            List<CertificateElementModel> elements = new List<CertificateElementModel>();
            elements = certXML.Root.Element("page").Elements("text").Select(
                x => new CertificateElementModel
                {
                    XMLId = (string)x.Attribute("id"),
                    //Name = (string)x.Attribute("id"),     These depend on which element it is
                    //Title = (string)x.Attribute("id"),    They're populated in the next step
                    Font_Type = (string)x.Attribute("font"),
                    Font_Size = (int)x.Attribute("size"),
                    Font_Style = (string)x.Attribute("style"),
                    Color_Alpha = (int)x.Attribute("alpha"),
                    Color_Red = (int)x.Attribute("red"),
                    Color_Green = (int)x.Attribute("green"),
                    Color_Blue = (int)x.Attribute("blue"),
                    Text_Align = (string)x.Attribute("align"),
                    X_Coordinate = (int)x.Attribute("x"),
                    Y_Coordinate = (int)x.Attribute("y"),
                    Text = (string)x.Value,
                    Text_is_static = false,
                    Disabled = false

                }).ToList();

            // Set the elements for the exisiting certificate
            foreach(CertificateElementModel el in elements)
            {
                switch(el.XMLId)
                {
                    case "greet":
                        existingCert.Greet = el;
                        existingCert.Greet.Name = "Greet";
                        existingCert.Greet.Title = "Greeting";
                        break;

                    case "fullname":
                        existingCert.Fullname = el;
                        existingCert.Fullname.Name = "Fullname";
                        existingCert.Fullname.Title = "Learner Name";
                        existingCert.Fullname.Text_is_static = true;
                        break;

                    case "course":
                        existingCert.Course = el;
                        existingCert.Course.Name = "Course";
                        existingCert.Course.Title = "Course Title";
                        existingCert.Course.Text_is_static = true;
                        break;

                    case "completed":
                        existingCert.Completed = el;
                        existingCert.Completed.Name = "Completed";
                        existingCert.Completed.Title = "Completed Date";
                        existingCert.Completed.Text_is_static = true;
                        break;

                    case "duration":
                        existingCert.Duration = el;
                        existingCert.Duration.Name = "Duration";
                        existingCert.Duration.Title = "Course Duration";
                        existingCert.Duration.Text_is_static = true;
                        break;

                    case "statement":
                        if (existingCert.Statement1 == null)
                        {
                            existingCert.Statement1 = el;
                            existingCert.Statement1.Name = "Statement1";
                            existingCert.Statement1.Title = "Statement";
                            break;
                        }

                        else if (existingCert.Statement2 == null)
                        {
                            existingCert.Statement2 = el;
                            existingCert.Statement2.Name = "Statement2";
                            existingCert.Statement2.Title = "Statement";
                            break;
                        }

                        else if (existingCert.Statement3 == null)
                        {
                            existingCert.Statement3 = el;
                            existingCert.Statement3.Name = "Statement3";
                            existingCert.Statement3.Title = "Statement";
                            break;
                        }

                        else
                        {
                            existingCert.Statement4 = el;
                            existingCert.Statement4.Name = "Statement4";
                            existingCert.Statement4.Title = "Statement";
                            break;
                        }

                    default:
                        break;
                }
            }

            // Create a disabled statement element with default values if not found in the XML
            CertificateElementModel disabledStatement = new CertificateElementModel()
            {
                XMLId = "statement",
                //Name = "Statement",   This value set below
                Title = "Statement",
                Font_Type = "Helvetica",
                Font_Size = 20,
                Font_Style = "Normal",
                Color_Alpha = 0,
                Color_Red = 0,
                Color_Green = 0,
                Color_Blue = 0,
                Text_Align = "Center",
                X_Coordinate = 20,
                //Y_Coordinate = 0,     This value set below
                Text = "",
                Text_is_static = false,
                Disabled = true
            };

            if(existingCert.Statement4 == null)
            {
                existingCert.Statement4 = disabledStatement;
                existingCert.Statement4.Name = "Statement4";
                existingCert.Statement4.Y_Coordinate = 456;
            }

            if (existingCert.Statement3 == null)
            {
                existingCert.Statement3 = disabledStatement;
                existingCert.Statement3.Name = "Statement3";
                existingCert.Statement3.Y_Coordinate = 399;
            }
            if (existingCert.Statement2 == null)
            {
                existingCert.Statement2 = disabledStatement;
                existingCert.Statement2.Name = "Statement2";
                existingCert.Statement2.Y_Coordinate = 342;
            }
            if (existingCert.Statement1 == null)
            {
                existingCert.Statement1 = disabledStatement;
                existingCert.Statement1.Name = "Statement1";
                existingCert.Statement1.Y_Coordinate = 285;
            }


            return existingCert;
        }

        public static int SaveAndContinue(CertificateModel c)
        {
            int CertId;

            SqlParameter[] p = ProcessCertificate(c);


            using (TestDbContext db = new TestDbContext())
            {
                CertId = db.Database.SqlQuery<int>("exec Certificate_SaveAndContinue @CertId, @Coursekey, @UserGroupKey, @CertificateTemplate, @CreatedBy", p).FirstOrDefault();
            }

            return CertId;
        }

        public static void SaveAndExit(CertificateModel c)
        {

            SqlParameter[] p = ProcessCertificate(c);


            using (TestDbContext db = new TestDbContext())
            {
                db.Database.ExecuteSqlCommand("exec Certificate_SaveAndExit @CertId, @Coursekey, @UserGroupKey, @CertificateTemplate, @CreatedBy", p);
            }
        }

        public static SqlParameter[] ProcessCertificate(CertificateModel c)
        {
            string createdBy = "test_user";  // This would be the logged-in admin

            // Create a list of CertificateElementModel for xml build below
            List<CertificateElementModel> elements = new List<CertificateElementModel>();
            elements.Add(c.Greet);
            elements.Add(c.Fullname);
            elements.Add(c.Course);
            elements.Add(c.Completed);
            elements.Add(c.Duration);
            if (!c.Statement1.Disabled)
            {
                elements.Add(c.Statement1);
            }
            if (!c.Statement2.Disabled)
            {
                elements.Add(c.Statement2);
            }
            if (!c.Statement3.Disabled)
            {
                elements.Add(c.Statement3);
            }
            if (!c.Statement4.Disabled)
            {
                elements.Add(c.Statement4);
            }
            // Build xml for db
            var xml = new XElement("root",
                new XElement("page",
                    new XAttribute("name", c.Page_name),
                    new XAttribute("orientation", c.Orientation),
                    new XElement("image",
                        new XAttribute("src", c.Image_src)),
                    elements.Select(element =>
                            new XElement("text",
                                new XAttribute("id", element.XMLId),
                                new XAttribute("font", element.Font_Type),
                                new XAttribute("size", element.Font_Size),
                                new XAttribute("style", element.Font_Style),
                                new XAttribute("alpha", element.Color_Alpha),
                                new XAttribute("red", element.Color_Red),
                                new XAttribute("green", element.Color_Green),
                                new XAttribute("blue", element.Color_Blue),
                                new XAttribute("align", element.Text_Align),
                                new XAttribute("x", element.X_Coordinate),
                                new XAttribute("y", element.Y_Coordinate),
                                element.Text
                          ))
                    )
                );

            SqlParameter[] p = new SqlParameter[]{
                     new SqlParameter(){
                        ParameterName = "CertId",
                        SqlDbType = SqlDbType.Int,
                        Value = c.Id ?? (object)DBNull.Value
                    },

                     new SqlParameter(){
                        ParameterName = "Coursekey",
                        SqlDbType = SqlDbType.Int,
                        Value = c.Coursekey ?? (object)DBNull.Value
                    },
    
                     new SqlParameter(){
                        ParameterName = "UserGroupKey",
                        SqlDbType = SqlDbType.Int,
                        Value = c.UserGroupkey
                    },
                     new SqlParameter(){
                        ParameterName = "CertificateTemplate",
                        SqlDbType = SqlDbType.Text,
                        Value = xml.ToString()
                    },
                     new SqlParameter(){
                        ParameterName = "CreatedBy",
                        SqlDbType = SqlDbType.NVarChar,
                        Value = createdBy
                    }
            };

            return p;
        }

        public static IEnumerable<CertListItem> GetCertList(int UserGroupKey)
        {
            List<CertListItem> item = new List<CertListItem>();

            using (TestDbContext db = new TestDbContext())
            {
                SqlParameter p = new SqlParameter()
                {
                    ParameterName = "UserGroupKey",
                    SqlDbType = SqlDbType.Int,
                    Value = UserGroupKey
                };

                item = db.Database.SqlQuery<CertListItem>("EXEC GetCertificatesList @UserGroupKey", p).ToList();
            }

            // XDocument doc = XDocument.Load()

            return item;
        }

        public static void DeleteCertificate(int CertId)
        {
            using (TestDbContext db = new TestDbContext())
            {
                SqlParameter p = new SqlParameter()
                {
                    ParameterName = "CertificateId",
                    SqlDbType = SqlDbType.Int,
                    Value = CertId
                };

                db.Database.ExecuteSqlCommand("exec Certificate_Delete @CertificateId", p);
            }
        }

        private static CertificateModel NewCertificate()
        {
            CertificateModel _certificate = new CertificateModel()
            {
                Id = null,
                Coursekey = null,  // default to no target course
                UserGroupkey = 1,  // This would eventually come from the logged-in admin's highest level team
                Page_name = "Sample Certificate", // Should be blank and required to be provided by admin
                Orientation = "Landscape",  // Should this be set by admin or determined by dimension of image?
                Image_src = "sample.png",  // a default image, to be changed by admin

                Greet = new CertificateElementModel()
                {
                    XMLId = "greet",
                    Name = "Greet",
                    Title = "Greeting",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 0,
                    Text = "",
                    Text_is_static = false,
                    Disabled = false
                },

                Fullname = new CertificateElementModel()
                {
                    XMLId = "fullname",
                    Name = "Fullname",
                    Title = "Learner Name",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 57,
                    Text = "",
                    Text_is_static = true,
                    Disabled = false
                },


                Course = new CertificateElementModel()
                {
                    XMLId = "course",
                    Name = "Course",
                    Title = "Course Title",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 114,
                    Text = "",
                    Text_is_static = true,
                    Disabled = false
                },

                Completed = new CertificateElementModel()
                {
                    XMLId = "completed",
                    Name = "Completed",
                    Title = "Completed Date",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 171,
                    Text = "",
                    Text_is_static = true,
                    Disabled = false
                },
                Duration = new CertificateElementModel()
                {
                    XMLId = "duration",
                    Name = "Duration",
                    Title = "Course Duration",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 228,
                    Text = "",
                    Text_is_static = true,
                    Disabled = false
                },
                Statement1 = new CertificateElementModel()
                {
                    XMLId = "statement",
                    Name = "Statement1",
                    Title = "Statement",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 285,
                    Text = "",
                    Text_is_static = false,
                    Disabled = false
                },
                Statement2 = new CertificateElementModel()
                {
                    XMLId = "statement",
                    Name = "Statement2",
                    Title = "Statement",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 342,
                    Text = "",
                    Text_is_static = false,
                    Disabled = false
                },
                Statement3 = new CertificateElementModel()
                {
                    XMLId = "statement",
                    Name = "Statement3",
                    Title = "Statement",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 399,
                    Text = "",
                    Text_is_static = false,
                    Disabled = false
                },
                Statement4 = new CertificateElementModel()
                {
                    XMLId = "statement",
                    Name = "Statement4",
                    Title = "Statement",
                    Font_Type = "Helvetica",
                    Font_Size = 20,
                    Font_Style = "Normal",
                    Color_Alpha = 0,
                    Color_Red = 0,
                    Color_Green = 0,
                    Color_Blue = 0,
                    Text_Align = "Center",
                    X_Coordinate = 20,
                    Y_Coordinate = 456,
                    Text = "",
                    Text_is_static = false,
                    Disabled = true
                }
            };
            return _certificate;
        }

    }
}