using System.Collections;
using System.Collections.Generic;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.CSVData
{ 
    public class CsvNode //Keep in mind, all the data is numerical. We may have to add some special conditions for this
                         // in the future.
    {
        public List<double> States = new List<double>();
        public double CurrentState;
        public PartialLeastSquaresPredictionModel PredictionModel;
        public string Name;

    }
}