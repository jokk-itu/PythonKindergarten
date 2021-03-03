using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;

namespace MiniTwitApi.Tests.Repositories
{
    public class DbTest
    {
        private readonly SqliteConnection _connection;
        protected readonly Context _context;
        
        protected DbTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var builder = new DbContextOptionsBuilder<Context>().UseSqlite(_connection);
            if (!builder.IsConfigured)
                throw new Exception("Deactivate SqlServer connection string");
            
            _context = new Context(builder.Options);
            _context.Database.EnsureCreated();
        }

        public void Dispose() => _connection.Close();
    }
}