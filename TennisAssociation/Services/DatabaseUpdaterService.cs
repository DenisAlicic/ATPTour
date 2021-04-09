using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TennisAssociation.Controllers;
using TennisAssociation.Interfaces;
using TennisAssociation.Models;
using TennisAssociation.Utils;

namespace TennisAssociation.Services
{
    public class DatabaseUpdaterService<T> : IDatabaseUpdater<T> where T : class
    {
        private readonly string _scriptName;
        private readonly string _scriptDirectory;
        private readonly string _pathToFile;
        private ICsvReader _reader;
        private readonly TennisAssociationContext _db;
        private Func<List<string>, Dictionary<string, int>, T> _builder;
        private List<T> _itemsForAdding;
        
        public DatabaseUpdaterService(string scriptDirectory, string scriptName, string pathToFile, TennisAssociationContext context, Func<List<string>, Dictionary<string, int >, T> builder)
        {
            this._pathToFile = pathToFile;
            this._scriptName = scriptName;
            _reader = new CsvReader(pathToFile);
            _scriptDirectory = scriptDirectory; 
            _db = context;
            _builder = builder;
            _itemsForAdding = new List<T>();
        }
        
        public bool PrepareData()
        {
            Dictionary<string, int> columnNumbers = _reader.GetColumns();
            List<List<string>> rows = _reader.GetRows();
            if (columnNumbers is null || rows is null)
            {
                return false;
            }
            
            foreach (var row in rows)
            {
                T t = _builder.Invoke(row, columnNumbers); 
                
                _itemsForAdding.Add(t);
            }
            
            return true;
        }

        public bool UpdateData(DbSet<T> changingSet, string tableName)
        {
            _db.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tableName);    
                        
            foreach (var t in _itemsForAdding)
            {
                changingSet.Add(t);
            }

            _db.SaveChanges();

            return true;
        }
    }
}