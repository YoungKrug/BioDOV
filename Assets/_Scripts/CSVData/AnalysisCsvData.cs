using System;
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
              //Test();
             // return;
             string target = "CCL5";
             int targetIndex = 0;
             int index = csv.Data.FindIndex(x => x.Name == target);
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
            // double[][] outputs = new double[targetList.Count][];  // Your target variable data as a 1D array
             // Number of latent components (similar to the 'n_components' in Python's PLS model)
             int numberOfComponents = 4; // Adjust this value based on your preference or model evaluation
          
             // Create the PLS model and specify the number of latent components
          
            PartialLeastSquaresAnalysis pls = new PartialLeastSquaresAnalysis  {
                Method = AnalysisMethod.Center,
                Algorithm = PartialLeastSquaresAlgorithm.NIPALS
            };
            var dataR = pls.Learn(inputs, outputs);
            double[] newSampleChanges = new double[csv.Data.Count - 1];

            // Normalize the feature vector and make predictions
            double[][] newSampleInput = new double[][] { newSampleChanges };
            double[][] predictions = dataR.Transform(newSampleInput);
            // Fit the model to the data
            //              pls.Learn(reducedInputs,);
            //
// // Assuming 'newSampleChanges' contains the new feature vector with changed states
//              double[] newSampleChanges = new double[] { 2, 0, /*...and so on*/ };
//
// // Reduce the new sample to the same number of components as used in training
//              double[] newSampleReduced = newSampleChanges.Dot(truncatedSingularVectors).Dot(truncatedDiagonalMatrix);
//
// // Normalize the feature vector and make predictions
//              double[][] predictions = pls.Transform(new double[][] { newSampleReduced });
//
// // Extract the prediction for 'CCL5' (assuming it's the first target variable)
//              double ccl5Prediction = predictions[0][0];

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