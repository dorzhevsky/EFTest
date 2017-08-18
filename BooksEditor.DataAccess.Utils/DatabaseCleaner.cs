using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BooksEditor.DataAccess.Utils
{
    internal class DatabaseCleaner : ModelTraversal
    {
        private readonly StringBuilder sqlQueryBuilder;
        private readonly DbContext context;

        public DatabaseCleaner(DbContext context) : base(context.Model)
        {
            this.context = context;
            sqlQueryBuilder = new StringBuilder();
        }

        protected override void Visit(IEntityType obj)
        {
            IRelationalEntityTypeAnnotations relational = obj.Relational();
            sqlQueryBuilder.AppendLine($"DELETE FROM {relational.TableName};");
        }

        public void Clean()
        {
            Traverse();
            context.Database.ExecuteSqlCommand(sqlQueryBuilder.ToString());
        }
    }
}