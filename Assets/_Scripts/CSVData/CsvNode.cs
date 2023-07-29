using System.Collections;
using System.Collections.Generic;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.CSVData
{
    public struct CasualtyInformation // 15 -> 20 bytes
    {
        public string Name;
        public double CasualtyPercent;

    }
    public class CsvNode
    {
        public List<double> States = new List<double>();
        public double CurrentState;
        public PartialLeastSquaresPredictionModel PredictionModel;
        public string Name;

    }
}