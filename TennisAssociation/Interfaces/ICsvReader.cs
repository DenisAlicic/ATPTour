using System.Collections.Generic;

namespace TennisAssociation.Interfaces
{
    public interface ICsvReader
    {
        Dictionary<string, int> GetColumns();
        List<List<string>> GetRows();
    }
}