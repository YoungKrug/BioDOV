﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.Experimental.GraphView;
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
        public void TurnCausalityDataIntoCsv()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var node in Data)
            {
           
                foreach (var casualty in node.CasualtyInformationList)
                {
                    string name = $"{node.Name}->{casualty.Name}";
                    string value = $"{casualty.CasualtyPercent}";
                    builder.Append($"{name},{value}\n");
                }
            }
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(builder.ToString());
                fs.Write(info, 0, info.Length);
            }
            
        }
    }
}