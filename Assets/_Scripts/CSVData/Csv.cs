using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Scripts.CSVData
{
    public class Csv
    {
        public List<List<CsvNodes>> Data = new List<List<CsvNodes>>();

        public void PrintData()
        {
            var matrix = Data;
            StringBuilder data = new StringBuilder();
            foreach (var column in matrix)
            {
                foreach (var row in column)
                {
                    data.Append($"{row.Name} -> {row.States[0]}, ");
                }

                data.Append("\n");
            }
            Debug.Log(data);
        }
    }
}