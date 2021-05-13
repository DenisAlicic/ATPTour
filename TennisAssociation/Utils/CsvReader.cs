using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.VisualBasic.FileIO;
using TennisAssociation.Interfaces;

namespace TennisAssociation.Utils
{
    /// <summary>
    /// Implementation of reading CSV files.
    /// </summary>
    public class CsvReader : ICsvReader
    {
        private Dictionary<string, int> columnNumbers;
        private List<List<string>> rows;
        private TextFieldParser parser;

        public CsvReader(string path)
        {
            columnNumbers = new Dictionary<string, int>();
            rows = new List<List<string>>();
            parser = new TextFieldParser(path);
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = false;
        }

        ~CsvReader()
        {
            parser.Close();
        }

        public Dictionary<string, int> GetColumns()
        {
            try
            {
                string[] colFields = parser.ReadFields();
                for (int i = 0; i < colFields.Length; i++)
                {
                    columnNumbers[colFields[i]] = i;
                }

                return columnNumbers;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return null;
        }

        public List<List<string>> GetRows()
        {
            try
            {
                while (!parser.EndOfData)
                {
                    string[] fieldData = parser.ReadFields();
                    //Making empty value as null
                    for (var i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }

                    rows.Add(fieldData.ToList());
                }

                return rows;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return null;
        }
    }
}