using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooksEditor.DataAccess.Tests
{
    [TestClass]
    public class SetupAssemblyInitializer
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            BooksContext db = new BooksContext(DatabaseContextOptions.Create());
            db.Database.EnsureDeleted();
            db.Database.Migrate();
        }
    }
}