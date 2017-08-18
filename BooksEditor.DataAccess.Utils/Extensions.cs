using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BooksEditor.DataAccess.Utils
{
    public static class Extensions
    {
        public static void DetachAll(this DbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToList();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State != EntityState.Detached)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }

        public static void DeleteAll(this DbContext context)
        {
            DatabaseCleaner cleaner = new DatabaseCleaner(context);
            cleaner.Clean();
        }
    }
}