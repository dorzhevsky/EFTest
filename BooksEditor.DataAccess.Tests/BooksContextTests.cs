using System;
using System.Linq;
using BooksEditor.DataAccess.Utils;
using BooksEditor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooksEditor.DataAccess.Tests
{
    [TestClass]
    public class BooksContextTests
    {
        private BooksContext context;

        [TestInitialize]
        public void Setup()
        {
            context = new BooksContext(DatabaseContextOptions.Create());
            Clean();           
        }

        [TestCleanup]
        public void CleanUp()
        {
            context?.Dispose();
        }

        private void Clean()
        {
           context.DeleteAll();
        }

        [TestMethod]
        public void ShouldCreateCategory()
        {
            Category category = new Category {CategoryName = "Name"};
            var trackingEntity = context.Add(category);
            Assert.AreEqual(trackingEntity.State, EntityState.Added);
            Assert.IsNull(category.TimeStamp);
            Assert.IsTrue(category.Id < 0);

            context.SaveChanges();

            Assert.AreEqual(trackingEntity.State, EntityState.Unchanged);
            Assert.IsNotNull(category.TimeStamp);
            Assert.IsTrue(category.Id > 0);
            Assert.AreEqual(1, context.Categories.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldRaiseExecptionDueToAbsentCategoryDescriptionInResultSet()
        {
            Category category = new Category { CategoryName = "Name" };
            context.Add(category);
            context.SaveChanges();

            context.DetachAll();

            context.Categories
                   .FromSql("SELECT Id, CategoryName, TimeStamp FROM dbo.Categories")
                   .Load();
        }

        [TestMethod]
        public void ShouldExecuteRawSql()
        {
            Category category = new Category { CategoryName = "Name" };
            context.Add(category);
            context.SaveChanges();

            context.DetachAll();

            var dbCategory = context.Categories
                .FromSql("SELECT * FROM dbo.Categories")
                .AsNoTracking()
                .FirstOrDefault();

            Assert.IsNotNull(dbCategory);
        }

        [TestMethod]
        public void ShouldLoadRelatedCategoryEntity()
        {
            Category category = new Category { CategoryName = "Name" };
            context.Add(category);

            Product product = new Product {Category = category, Name = "Product"};
            context.Add(product);

            context.SaveChanges();

            context.DetachAll();

            var dbProduct = context.Products.Include(c => c.Category)
                .FirstOrDefault();

            Assert.IsNotNull(dbProduct.Category);
        }

        [TestMethod]
        public void ShouldNotLoadRelatedCategoryEntity()
        {
            Category category = new Category { CategoryName = "Name" };
            context.Add(category);

            Product product = new Product { Category = category, Name = "Product" };
            context.Add(product);

            context.SaveChanges();

            context.DetachAll();

            var dbProduct = context.Products
                .FirstOrDefault();

            Assert.IsNull(dbProduct.Category);
        }


        [TestMethod]
        public void ShouldLoadRelatedProductsEntity()
        {
            Category category = new Category { CategoryName = "Name" };
            context.Add(category);

            Product product = new Product { Category = category, Name = "Product" };
            context.Add(product);

            context.SaveChanges();

            context.DetachAll();

            var dbCategory = context.Categories.Include(c => c.Products)
                .FirstOrDefault();

            Assert.AreEqual(1, dbCategory.Products.Count);
        }
    }
}