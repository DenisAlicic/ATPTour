using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace TennisAssociation.Interfaces
{
    public interface IDatabaseUpdater
    {
        bool UpdateData();
    }
}