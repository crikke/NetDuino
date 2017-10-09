using NetDuino.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NetDuino.Tests
{
    class TestApplicationDbContext : IApplicationDbContext
    {
        public TestApplicationDbContext()
        {
            this.Arduinos = new TestDbSet<ArduinoModel>();
            this.Components = new TestDbSet<Component>();
        }

        public DbSet<ArduinoModel> Arduinos { get; set; }
        public DbSet<Component> Components { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void MarkAsModified(IDbEntry item) { }
        public void Dispose() { }
    }
}
