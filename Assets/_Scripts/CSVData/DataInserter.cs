using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CSVData
{
    public class DataInserter : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private TextAsset csvAsset;
        [SerializeField] private Csv _csv;

        private void Start()
        {
            OnInsertEvent();
        }

        private void OnInsertEvent()
        {
            Csv csv = new Csv();
            List<CsvNodes> nodesList = new List<CsvNodes>();
            string[] rowValues = csvAsset.text.Split("\n");
            List<string> columnValues = rowValues[0].Split(",").ToList();
            foreach (var csvValue in columnValues)
            {
                nodesList.Add(new CsvNodes
                {
                    Name = csvValue
                });
            }
            for (int i = 1 ; i < columnValues.Count; i++)
            {
                columnValues = rowValues[i].Split(",").ToList();
                for (int j = 0; j < columnValues.Count; j++)
                {
                    nodesList[j].States.Add(columnValues[j]);
                }
            }
            csv.Data = nodesList;
            new AnalysisCsvData().FindCasualtyInData(csv);
            csv.TurnCausalityDataIntoCsv();
        }

        

    }
}
