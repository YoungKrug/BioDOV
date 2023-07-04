using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Scripts.CSVData
{
    public class Csv
    {
        public List<CsvNodes> Data = new List<CsvNodes>();

        public void PrintData()
        {
            var matrix = Data;
            StringBuilder data = new StringBuilder();
            foreach (var node  in matrix)
            {
             
                data.Append($"{node.Name} -> {node.States[0]}, ");
                data.Append("\n");
            }
            Debug.Log(data);
        }
    }
}