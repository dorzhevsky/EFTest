using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BooksEditor.DataAccess.Utils
{
    internal abstract class ModelTraversal
    {
        readonly HashSet<IEntityType> traversedEntities = new HashSet<IEntityType>();
        protected readonly IModel model;

        protected ModelTraversal(IModel model)
        {
            this.model = model;
        }

        public void Traverse()
        {
            IEnumerable<IEntityType> types = model.GetEntityTypes();
            foreach (IEntityType entityType in types)
            {
                Traverse(entityType);
            }
        }

        private void Traverse(IEntityType entityType)
        {
            if (!traversedEntities.Contains(entityType))
            {
                var foreignKeys = entityType.GetReferencingForeignKeys();
                TraverseChildren(foreignKeys);
                TraverseSelf(entityType);
            }
        }

        private void TraverseChildren(IEnumerable<IForeignKey> foreignKeys)
        {
            foreach (IForeignKey foreignKey in foreignKeys)
            {
                IEntityType child = foreignKey.DeclaringEntityType;
                TraverseSelf(child);
            }

        }

        private void TraverseSelf(IEntityType entityType)
        {
            Visit(entityType);
            traversedEntities.Add(entityType);
        }

        protected abstract void Visit(IEntityType entityType);
    }
}