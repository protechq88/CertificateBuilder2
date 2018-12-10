using System.Data.Entity;

namespace CertificateBuilder2.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
            : base("TestDb")
        { }

        public static TestDbContext Create()
        {
            return new TestDbContext();
        }
    }
}