using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TennisAssociation.Interfaces
{
    /// <summary>
    /// Generic interface for manipulating data in database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDatabaseUpdater<T> where T : class
    {
        /// <summary>
        /// Apply updating table in database.
        /// </summary>
        /// <param name="changingSet"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool UpdateData(DbSet<T> changingSet, string tableName);
        /// <summary>
        /// Preparing DbSet for applying.
        /// </summary>
        /// <returns></returns>
        bool PrepareData();
    }
}