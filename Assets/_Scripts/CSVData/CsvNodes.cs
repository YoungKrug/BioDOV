using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CSVData
{
    public struct CasualtyInformation // 15 -> 20 bytes
    {
        public string Name;
        public float CasualtyPercent;

    }
    public class CsvNodes
    {
        public List<string> States = new List<string>();
        public string Name;
        public List<CasualtyInformation> CasualtyInformationList = new List<CasualtyInformation>();

    }
}