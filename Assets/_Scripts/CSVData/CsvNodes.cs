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
    public class CsvNodes
    {
        public List<double> States = new List<double>();
        public string Name;
        public List<CasualtyInformation> CasualtyInformationList = new List<CasualtyInformation>();

    }
}