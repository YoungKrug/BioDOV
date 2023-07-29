using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Scripts.CSVData
{
    public class Csv
    {
        public string path = "Assets/SimulationFiles/datafile.csv";
        public List<CsvNode> Data = new List<CsvNode>();
        public int TotalNumberOfStates { get; private set; }
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

        public void NumberOfDataPoints()
        {
            int number = 0;
            foreach (var node in Data)
            {
                foreach (var state in node.States)
                {
                    number += 1;
                }
            }

            TotalNumberOfStates = number;
        }
    }
}