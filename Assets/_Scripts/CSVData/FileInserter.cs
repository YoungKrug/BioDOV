using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.Simulation;

namespace _Scripts.CSVData
{
    public class FileInserter
    {
        private readonly string _path;
        public FileInserter(string path)
        {
            _path = path;
        }
        public Csv ReadData()
        {
            string text = File.ReadAllText(_path);
            Csv csv = new Csv();
            List<CsvNode> nodesList = new List<CsvNode>();
            string[] rowValues = text.Split("\n");
            List<string> columnValues = rowValues[0].Split(",").ToList();
            foreach (var csvValue in columnValues)
            {
                nodesList.Add(new CsvNode
                {
                    Name = csvValue
                });
            }
            for (int i = 1 ; i < rowValues.Length; i++)
            {
                columnValues = rowValues[i].Split(",").ToList();
                for (int j = 0; j < columnValues.Count; j++)
                {
                    double val = 0;
                    if (!double.TryParse(columnValues[j], out val))
                    {
                        val = 0;
                    }
                    nodesList[j].States.Add(val);
                }
            }
            csv.Data = nodesList;
            csv.Data.RemoveRange(0,3);
            foreach (var node in csv.Data)
            {
                node.CurrentState = node.States[0]; // Set them all to there initial State
            }
            return csv;
        }
    }
}