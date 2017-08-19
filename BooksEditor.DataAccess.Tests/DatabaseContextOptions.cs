using BooksEditor.DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BooksEditor.DataAccess.Tests
{
    public static class DatabaseContextOptions
    {
        public static DbContextOptions<BooksContext> Create()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json");
            var config = configurationBuilder.Build();

            var builder = new DbContextOptionsBuilder<BooksContext>();
            builder.UseNpgsql(config.GetConnectionString("Db"));

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            builder.UseLoggerFactory(loggerFactory);

            return builder.Options;
        }
    }
}