﻿
using System.Collections.Generic;
using System.Linq;
using _Scripts.CSVData;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Statistics
{
    public class PartialLeastSquaresPredictionModel
    {
        private CsvNode _nodeToPredict;
        private PartialLeastSquaresAnalysis _partialLeastSquaresAnalysis;
        private MultivariateLinearRegression _linearRegressionModel;

        public PartialLeastSquaresPredictionModel(CsvNode node, Csv csv)
        {
            _nodeToPredict = node;
            Initialize(csv, node.Name);
        }

        public double Predict(double[] newStateChanges)
        {
            // Samples have to be == to features used (columns used)
            // Normalize the feature vector and make predictions
            double[][] newSampleInput = new double[][] {newStateChanges};
            double[][]
                predictions =
                    _linearRegressionModel.Transform(newSampleInput); // The transformation vector, needs a feature vector (**see above)
            return predictions[0][0];
        }
        private void Initialize(Csv csv, string target)
        {
              //   /*
            //    * Has to be used to predict,
            //    * Based on this state, and this state of this gene, how is this gene effected
            //    * Column names are the features **/
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
            
            MultivariateLinearRegression multivariateLinearRegression = pls.Learn(inputs, outputs); // this is the learned model that transforms the data
            _partialLeastSquaresAnalysis = pls;
            _linearRegressionModel = multivariateLinearRegression;
        }
    }
}