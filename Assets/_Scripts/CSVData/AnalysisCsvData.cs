using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using _Scripts.Statistics;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Statistics.Analysis;
using Accord.Statistics.Kernels;
using UnityEngine;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Models.Regression.Linear;
using Codice.Client.Common.TreeGrouper;
using MathNet.Numerics.LinearAlgebra.Double;
using Matrix = Accord.Math.Matrix;

namespace _Scripts.CSVData
{
    public class AnalysisCsvData
    {
        public string path = "Assets/SimulationFiles/";
        
        public void PartialLeastSquareRegression(Csv csv)
        {
            PartialLeastSquaresPredictionModel model = new PartialLeastSquaresPredictionModel(csv.Data[0], csv);
        }

        public void CreateTextFile(string text, string fileName)
        {
            string file = $"{path}/{fileName}.csv";
            using (FileStream fs = File.Create(file))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}