using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TennisAssociation.Interfaces
{
    public interface IDatabaseUpdater<T> where T : class
    {
        bool UpdateData(DbSet<T> changingSet, string tableName);
        bool PrepareData();
    }
}