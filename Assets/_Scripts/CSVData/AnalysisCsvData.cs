﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            //   /*
            //    * Has to be used to predict,
            //    * Based on this state, and this state of this gene, how is this gene effected
            //    * Column names are the features **/
            string target = "CCL5";
            // Your input feature data as a 2D array
            List<double> targetList = new List<double>();
            int statesCount = csv.Data[0].States.Count;
            int dataCount = csv.Data.Count;
            double[][] inputList = new double[statesCount][];
            for (int i = 0; i < statesCount; i++) //row
            {
                int indexNum = 0;
                double[] data = new double[dataCount - 1];
                for (int j = 0; j < dataCount; j++) //column
                {
                    double val = csv.Data[j].States[i];
                    if (csv.Data[j].Name == target)
                    {
                        targetList.Add(val);
                        continue;
                    }
                    data[indexNum++] = val;
                }
                inputList[i] = data;
            }

            double[][] inputs = inputList.Select(row => row.Take(row.Length - 1).ToArray()).ToArray();
            double[][] outputs = new double[statesCount][];
            for (int i = 0; i < statesCount; i++)
            {
                outputs[i] = new double[] { targetList[i] };
            }
            // Create the PLS model and specify the number of latent components

            PartialLeastSquaresAnalysis pls = new PartialLeastSquaresAnalysis
            {
                Method = AnalysisMethod.Center,
                Algorithm = PartialLeastSquaresAlgorithm.NIPALS //The more indepth model **
            };
            MultivariateLinearRegression dataR = pls.Learn(inputs, outputs); // this is the learned model that transforms the data
            double[]
                newSampleChanges =
                    new double[csv.Data.Count - 1]; // Samples have to be == to features used (columns used)
            // Normalize the feature vector and make predictions
            double[][] newSampleInput = new double[][] { newSampleChanges };
            double[][]
                predictions =
                    dataR.Transform(newSampleInput); // The transformation vector, needs a feature vector (**see above)
            double prediction = predictions[0][0];
            Debug.Log(prediction);// this is the prediction 
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