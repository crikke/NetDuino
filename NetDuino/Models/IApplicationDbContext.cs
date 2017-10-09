using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace NetDuino.Models
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<ArduinoModel> Arduinos { get; }
        DbSet<Component> Components { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void MarkAsModified(IDbEntry item);
    }
}