using System.Collections.Generic;

namespace TennisAssociation.Interfaces
{
    /// <summary>
    /// Interface for reading CSV files.
    /// </summary>
    public interface ICsvReader
    {
        /// <summary>
        /// Read columns and maps in position number.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> GetColumns();
        /// <summary>
        /// Read all rows in CSV file.
        /// </summary>
        /// <returns></returns>
        List<List<string>> GetRows();
    }
}