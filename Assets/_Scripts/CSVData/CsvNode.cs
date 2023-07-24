using System.Collections;
using System.Collections.Generic;
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
        public Vector2 Coords;
        public List<CasualtyInformation> CasualtyInformationList = new List<CasualtyInformation>();
        public string Name;

    }
}